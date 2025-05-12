import { Component, Inject } from '@angular/core';
import { ToggleOption } from '../chip-selector.component';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';

interface DialogData {
  options: ToggleOption[];
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
    MatButtonModule
  ],
  templateUrl: './chip-selector-dialog.component.html',
  styleUrl: './chip-selector-dialog.component.scss'
})
export class ChipSelectorDialogComponent {

  tempSelected: any[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private dialogRef: MatDialogRef<ChipSelectorDialogComponent>
  ) {
    this.tempSelected = [...data.selected];
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
