import { Component, Host, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShellComponent } from '../../shell/shell/shell.component';

@Component({
  selector: 'app-public-leagues-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './public-list.component.html',
  styleUrls: ['./public-list.component.scss']
})
export class PublicListComponent {
  
  constructor(@Host() @SkipSelf() private shell: ShellComponent) {}

  onSelect(id: number) {
    this.shell.onSelectItem(id);
  }
  
}
