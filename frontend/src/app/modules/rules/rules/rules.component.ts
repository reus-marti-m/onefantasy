import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-rules',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './rules.component.html',
  styleUrl: './rules.component.scss'
})
export class RulesComponent {

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) { }

  close() {
    this.router.navigate(
      [{ outlets: { modal: null } }],
      { relativeTo: this.route.parent }
    );
  }

}
