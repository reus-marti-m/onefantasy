import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-preferences',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './preferences.component.html',
  styleUrl: './preferences.component.scss'
})
export class PreferencesComponent {

  constructor(private shell: ShellComponent) { }

  close() {
    this.shell.fullScreen = null;
  }

}
