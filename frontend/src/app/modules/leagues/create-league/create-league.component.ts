import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-league',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './create-league.component.html',
  styleUrl: './create-league.component.scss'
})
export class CreateLeagueComponent {

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
