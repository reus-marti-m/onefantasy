import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { ChipSelectorDialogComponent } from './chip-selector-dialog/chip-selector-dialog.component';
import { ToggleOption } from '../detail.component';

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

  @Input() title = '';
  @Input() options: ToggleOption[] = [];
  @Input() selected: string[] = [];
  @Input() disabled = false;
  @Output() selectedChange = new EventEmitter<any[]>();
  @Input() actualResult: string[] = [];
  @Input() hasResult: boolean | undefined = false;

  constructor(private dialog: MatDialog) { }

  openDialog() {
    if (this.disabled) return;
    const ref = this.dialog.open(ChipSelectorDialogComponent, {
      data: {
        options: this.options,
        selected: this.selected,
        title: this.title
      }
    });
    ref.afterClosed().subscribe((res: string[] | undefined) => {
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

  getResultInfo(): string {
    if (this.actualResult.length === 0) {
      return "No ha ocorregut cap opció.";
    } else {
      return this.actualResult.join(", ");
    }
  }

}
