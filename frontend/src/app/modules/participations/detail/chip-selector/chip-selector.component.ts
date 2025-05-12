import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { ChipSelectorDialogComponent } from './chip-selector-dialog/chip-selector-dialog.component';

export interface ToggleOption {
  label: string;
  value: any;
  info?: string;
}

@Component({
  selector: 'app-chip-selector',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
  ],
  templateUrl: './chip-selector.component.html',
  styleUrl: './chip-selector.component.scss'
})
export class ChipSelectorComponent {

  @Input() title = 'Goleajdor';
  @Input() options: ToggleOption[] = [];
  @Input() selected: any[] = [];
  @Input() disabled = false;
  @Output() selectedChange = new EventEmitter<any[]>();
  @Input() actualResult!: any;

  constructor(private dialog: MatDialog) { }

  /** Obre la modal */
  openDialog() {
    if (this.disabled) return;
    const ref = this.dialog.open(ChipSelectorDialogComponent, {
      data: {
        options: this.options,
        selected: [...this.selected],
        title: this.title
      }
    });
    ref.afterClosed().subscribe((res: any[] | undefined) => {
      if (res) {
        this.selected = res;
        this.selectedChange.emit(this.selected);
      }
    });
  }

  getLabel(value: any) {
    return this.options.find(o => o.value === value)?.label;
  }

  removeChip(val: any) {
    if (this.disabled) return;
    this.selected = this.selected.filter(x => x !== val);
    this.selectedChange.emit(this.selected);
  }

  getInfo(val: any): string | undefined {
    return this.options.find(o => o.value === val)?.info;
  }

}
