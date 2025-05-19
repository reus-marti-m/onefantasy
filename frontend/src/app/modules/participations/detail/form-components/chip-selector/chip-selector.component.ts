import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { ChipSelectorDialogComponent } from './chip-selector-dialog/chip-selector-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ToggleOptionGroup } from '../../detail.component';

@Component({
  selector: 'app-chip-selector',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './chip-selector.component.html',
  styleUrl: './chip-selector.component.scss'
})
export class ChipSelectorComponent {

  @Input() title = '';
  @Input() optionGroups: ToggleOptionGroup[] = [];
  @Input() selected: string[] = [];
  @Input() disabled = false;
  @Output() selectedChange = new EventEmitter<any[]>();
  @Input() actualResult: [string, string][] = [];
  @Input() hasResult: boolean | undefined = false;
  @Input() score: string | undefined = '';
  @Input() budgetSpent = 0;
  @Input() budgetTotal = 0;

  constructor(private dialog: MatDialog) { }

  openDialog() {
    if (this.disabled) return;

    const oldSelection = [...this.selected];

    const ref = this.dialog.open(ChipSelectorDialogComponent, {
      data: {
        optionGroups: this.optionGroups,
        selected: this.selected,
        title: this.title,
        budgetSpent: this.budgetSpent,
        budgetTotal: this.budgetTotal
      }
    });

    ref.afterClosed().subscribe(res => {
      const dialogInst = ref.componentInstance as ChipSelectorDialogComponent;
      const newSelection = res !== undefined
        ? res
        : dialogInst.tempSelected;

      const equal = oldSelection.length === newSelection.length
        && oldSelection.every(v => newSelection.includes(v));

      if (!equal) {
        this.selected = newSelection;
        this.selectedChange.emit(this.selected);
      }
    });
  }

  getLabel(value: any): string | undefined {
    return this.optionGroups
      .flatMap(group => group.options)
      .find(o => o.value === value)
      ?.label;
  }

  removeChip(val: any) {
    if (this.disabled) return;
    this.selected = this.selected.filter(x => x !== val);
    this.selectedChange.emit(this.selected);
  }

  getInfo(val: any): string | undefined {
    return this.optionGroups
      .flatMap(group => group.options)
      .find(o => o.value === val)
      ?.info;
  }

  getResultInfo(): string {
    if (this.actualResult.length === 0) {
      return "No ha ocorregut cap opció.";
    } else {
      return this.actualResult
        .map(([, second]) => second)
        .join(", ");
    }
  }

  isActualResult(val: string): boolean {
    return this.actualResult.some(p => p[0] === val);
  }

}
