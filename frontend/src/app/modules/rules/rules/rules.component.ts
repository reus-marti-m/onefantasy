import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-rules',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './rules.component.html',
  styleUrl: './rules.component.scss'
})
export class RulesComponent {

constructor(private shell: ShellComponent) {}

  close() {
    this.shell.fullScreen = null;
  }

}
