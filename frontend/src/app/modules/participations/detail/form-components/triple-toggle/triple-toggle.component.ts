import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonToggleChange, MatButtonToggleModule } from '@angular/material/button-toggle';
import { ToggleOption } from '../../detail.component';

@Component({
  selector: 'app-triple-toggle',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonToggleModule
  ],
  templateUrl: './triple-toggle.component.html',
  styleUrl: './triple-toggle.component.scss'
})
export class TripleToggleComponent {

  @Input() title: string | null = null;
  @Input() options: ToggleOption[] = [];
  @Input() disabled = false;
  @Input() selected: any[] = [];
  @Output() selectedChange = new EventEmitter<any[]>();
  @Input() actualResult!: any;

  onSelectionChange(ev: MatButtonToggleChange) {
    const val = ev.value as any[];
    if (!this.disabled && val.length > 2) {
      val.shift();
    }
    this.selected = val;
    this.selectedChange.emit(this.selected);
  }

}
