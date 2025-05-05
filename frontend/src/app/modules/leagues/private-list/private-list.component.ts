import { Component, Host, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-private-leagues-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './private-list.component.html',
  styleUrls: ['./private-list.component.scss']
})
export class PrivateListComponent {

  constructor(@Host() @SkipSelf() private shell: ShellComponent) {}

  onSelect(id: number) {
    this.shell.onSelectItem(id);
  }
  
}
