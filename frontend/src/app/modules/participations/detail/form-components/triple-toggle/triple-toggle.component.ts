import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonToggleChange, MatButtonToggleModule } from '@angular/material/button-toggle';
import { ToggleOption } from '../../detail.component';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-triple-toggle',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonToggleModule,
    MatTooltipModule
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
  @Input() actualResult: [string, string][] = [];
  @Input() hasResult: boolean | undefined = false;
  @Input() score: string | undefined = '';

  onSelectionChange(ev: MatButtonToggleChange) {
    const val = ev.value as any[];
    if (!this.disabled && val.length > 2) {
      val.shift();
    }
    this.selected = val;
    this.selectedChange.emit(this.selected);
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

}
