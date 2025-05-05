import { Component, Host, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-participations-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {
  
  constructor(@Host() @SkipSelf() private shell: ShellComponent) {}

  onSelect(id: number) {
    this.shell.onSelectItem(id);
  }

}
