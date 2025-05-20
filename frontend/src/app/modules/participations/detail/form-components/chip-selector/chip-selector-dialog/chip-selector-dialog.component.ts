import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ToggleOption, ToggleOptionGroup } from '../../../detail.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';

interface DialogData {
  optionGroups: ToggleOptionGroup[];
  selected: any[];
  title?: string;
  budgetSpent: number;
  budgetTotal: number;
}

@Component({
  selector: 'app-chip-selector-dialog',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatCheckboxModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatIconModule
  ],
  templateUrl: './chip-selector-dialog.component.html',
  styleUrl: './chip-selector-dialog.component.scss'
})
export class ChipSelectorDialogComponent {

  tempSelected: any[];
  filterText: string = '';
  private originalSpent: number;
  totalBudget: number;
  private originalGroupSpent: number;
  hideOverBudget = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private dialogRef: MatDialogRef<ChipSelectorDialogComponent>
  ) {
    this.tempSelected = [...data.selected];
    this.originalSpent = data.budgetSpent;
    this.totalBudget = data.budgetTotal;
    this.originalGroupSpent = this.calculateGroupCost(data.selected);
  }

  private calculateGroupCost(vals: any[]): number {
    return vals.reduce((sum, val) => {
      const opt = this.data.optionGroups
        .flatMap(g => g.options)
        .find(o => o.value === val);
      return sum + (opt?.cost ?? 0);
    }, 0);
  }

  get currentGroupCost(): number {
    return this.calculateGroupCost(this.tempSelected);
  }

  get newSpentTotal(): number {
    return this.originalSpent - this.originalGroupSpent + this.currentGroupCost;
  }

  get isOverBudget(): boolean {
    return this.newSpentTotal > this.totalBudget;
  }

  get anyOptionMatches(): boolean {
    return this.data.optionGroups.some(g => this.hasOptions(g));
  }

  hasOptions(group: ToggleOptionGroup): boolean {
    return this.filteredOptions(group.options).length > 0;
  }

  toggleHideOverBudget() {
    this.hideOverBudget = !this.hideOverBudget;
  }

  shouldShowOption(opt: ToggleOption): boolean {
    if (this.tempSelected.includes(opt.value)) return true;
    if (!this.hideOverBudget) return true;
    const hypotheticalGroupCost = this.currentGroupCost + opt.cost;
    const hypotheticalNewTotal = this.originalSpent - this.originalGroupSpent + hypotheticalGroupCost;
    return hypotheticalNewTotal <= this.totalBudget;
  }

  optionsToDisplay(options: ToggleOption[]): ToggleOption[] {
    return this.filteredOptions(options)
      .filter(opt => this.shouldShowOption(opt));
  }

  filteredOptions(options: ToggleOptionGroup['options']): typeof options {
    const ft = this.filterText?.toLowerCase().trim();
    return ft
      ? options.filter(opt => opt.label.toLowerCase().includes(ft))
      : options;
  }

  onToggle(val: any) {
    const idx = this.tempSelected.indexOf(val);
    if (idx > -1) {
      this.tempSelected.splice(idx, 1);
    } else if (this.tempSelected.length < 2) {
      this.tempSelected.push(val);
    }
  }

  onDone() {
    this.dialogRef.close(this.tempSelected);
  }

}
