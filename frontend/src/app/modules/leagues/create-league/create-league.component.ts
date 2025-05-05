import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-create-league',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './create-league.component.html',
  styleUrl: './create-league.component.scss'
})
export class CreateLeagueComponent {

  constructor(private shell: ShellComponent) { }

  close() {
    this.shell.fullScreen = null;
  }

}
