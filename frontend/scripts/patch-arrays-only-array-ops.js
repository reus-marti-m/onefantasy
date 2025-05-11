// Parche per netejar api.ts

const fs = require('fs');
const filePath = 'src/app/core/api.ts';
let lines = fs.readFileSync(filePath, 'utf8').split('\n');

let inArrayProcessor = false;
let braceDepth = 0;

for (let i = 0; i < lines.length; i++) {
  const line = lines[i];

  if (!inArrayProcessor && /protected\s+process\w*\([^)]*\)\s*:\s*Observable<[^>]+\[\]>\s*\{/.test(line)) {
    inArrayProcessor = true;
    braceDepth = (line.match(/\{/g) || []).length - (line.match(/\}/g) || []).length;
    continue;
  }

  if (inArrayProcessor) {
    braceDepth += (line.match(/\{/g) || []).length;
    braceDepth -= (line.match(/\}/g) || []).length;

    lines[i] = lines[i].replace(
      /\}\s*else if\s*\(\s*status\s*!==\s*200\s*&&\s*status\s*!==\s*204\s*\)\s*\{/,
      '} else {'
    );

    if (/^\s*return\s+_observableOf<[^>]+>\(null\s+as\s+any\);/.test(lines[i])) {
      lines[i] = '';
    }

    if (braceDepth <= 0) {
      inArrayProcessor = false;
    }
  }
}

fs.writeFileSync(filePath, lines.join('\n'), 'utf8');
console.log('patch-arrays-only-array-ops applied to api.ts');
