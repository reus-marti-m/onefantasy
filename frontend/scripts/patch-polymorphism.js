// Parche per polimorfisme

const fs = require('fs');
const path = require('path');
const ts = require('typescript');

const FILE = path.resolve(__dirname, '../src/app/core/api.ts');

const configs = [
    {
        alias: 'ParticipationDtoResponse',
        elementType: 'ParticipationStandardDtoResponse',
        unionDef: `export type ParticipationDtoResponse =
  | ParticipationStandardDtoResponse
  | ParticipationExtraDtoResponse
  | ParticipationSpecialDtoResponse;`,
        namespaceDef: `
export namespace ParticipationDtoResponse {
  export function fromJS(item: any): ParticipationDtoResponse {
    switch(item.type) {
      case 0: return ParticipationStandardDtoResponse.fromJS(item);
      case 1: return ParticipationExtraDtoResponse.fromJS(item);
      case 2: return ParticipationSpecialDtoResponse.fromJS(item);
      default: throw new Error('Unknown participation type ' + item.type);
    }
  }
}`,
        excludeName: /(standard|extra|special)/i
    },
    {
        alias: 'MinigameDtoResponse',
        elementType: 'MinigameResultDtoResponse',
        unionDef: `export type MinigameDtoResponse =
  | MinigameResultDtoResponse
  | MinigameMatchDtoResponse
  | MinigameScoresDtoResponse
  | MinigamePlayersDtoResponse;`,
        namespaceDef: `
export namespace MinigameDtoResponse {
  export function fromJS(item: any): MinigameDtoResponse {
    switch(item.type) {
      case 1: return MinigameResultDtoResponse.fromJS(item);
      case 2: return MinigameMatchDtoResponse.fromJS(item);
      case 3: return MinigameScoresDtoResponse.fromJS(item);
      case 4: return MinigamePlayersDtoResponse.fromJS(item);
      default: throw new Error('Unknown minigame type ' + item.type);
    }
  }
}`,
        excludeName: /(result|match|scores|players)/i
    }
];

function transform(sourceText) {
    const sourceFile = ts.createSourceFile(FILE, sourceText, ts.ScriptTarget.Latest, true);
    let edits = [];
    function visit(node) {
        if (!(ts.isMethodSignature(node) || ts.isMethodDeclaration(node)) || !node.type) {
            return ts.forEachChild(node, visit);
        }
        const name = node.name.getText(sourceFile);
        const returnType = node.type.getText(sourceFile);

        configs.forEach(cfg => {
            const re = new RegExp(`Observable<${cfg.elementType}(\\[\\])?>`);
            if (!cfg.excludeName.test(name) && re.test(returnType)) {
                const isArray = /\[\]>$/.test(returnType);
                const newRet = `Observable<${cfg.alias}${isArray ? '[]' : ''}>`;
                edits.push({
                    span: { start: node.type.pos, end: node.type.end },
                    newText: newRet
                });
            }
        });

        ts.forEachChild(node, visit);
    }
    visit(sourceFile);

    let text = sourceText;
    edits
        .sort((a, b) => b.span.start - a.span.start)
        .forEach(e => {
            text = text.slice(0, e.span.start) + e.newText + text.slice(e.span.end);
        });

    configs.forEach(cfg => {
        text = text.replace(
            new RegExp(`\\b${cfg.elementType}\\.fromJS\\(item\\)`, 'g'),
            `${cfg.alias}.fromJS(item)`
        );
        text = text.replace(
            new RegExp(`\\b${cfg.elementType}\\.fromJS\\(resultData200\\)`, 'g'),
            `${cfg.alias}.fromJS(resultData200)`
        );
    });

    const hasAlias = configs.map(c => sourceText.includes(`type ${c.alias}`));
    const hasNamespace = configs.map(c => sourceText.includes(`namespace ${c.alias}`));

    const lines = text.split(/\r?\n/);
    let lastImport = -1;
    for (let i = 0; i < lines.length; i++) {
        if (/^\s*import\b/.test(lines[i])) lastImport = i;
    }

    const toInsert = [];
    configs.forEach((cfg, idx) => {
        if (!hasAlias[idx]) toInsert.push(cfg.unionDef);
        if (!hasNamespace[idx]) toInsert.push(cfg.namespaceDef);
    });

    if (toInsert.length) {
        if (lastImport >= 0) {
            const block = [''].concat(toInsert).concat(['']);
            lines.splice(lastImport + 1, 0, ...block);
            text = lines.join('\n');
        } else {
            text = toInsert.join('\n\n') + '\n\n' + text;
        }
    }

    return text;
}

const src = fs.readFileSync(FILE, 'utf8');
const out = transform(src);
fs.writeFileSync(FILE, out, 'utf8');
console.log('patch-polymorphism applied.');
