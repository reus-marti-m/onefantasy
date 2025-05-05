import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {

  constructor(private shell: ShellComponent) {}

  close() {
    this.shell.fullScreen = null;
  }
  
}
