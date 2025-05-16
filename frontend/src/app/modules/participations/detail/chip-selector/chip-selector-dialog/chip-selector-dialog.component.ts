import { Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ToggleOptionGroup } from '../../detail.component';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

interface DialogData {
  optionGroups: ToggleOptionGroup[];
  selected: any[];
  title?: string;
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
    MatInputModule
  ],
  templateUrl: './chip-selector-dialog.component.html',
  styleUrl: './chip-selector-dialog.component.scss'
})
export class ChipSelectorDialogComponent {

  tempSelected: any[];
  filterText: string = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private dialogRef: MatDialogRef<ChipSelectorDialogComponent>
  ) {
    this.tempSelected = [...data.selected];
  }

  hasOptions(group: ToggleOptionGroup): boolean {
    return this.filteredOptions(group.options).length > 0;
  }

  get anyOptionMatches(): boolean {
    return this.data.optionGroups.some(g => this.hasOptions(g));
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

  onSave() {
    this.dialogRef.close(this.tempSelected);
  }

  onCancel() {
    this.dialogRef.close();
  }

}
