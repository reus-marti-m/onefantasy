import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-public-leagues-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './public-list.component.html',
  styleUrls: ['./public-list.component.scss']
})
export class PublicListComponent {
  
  constructor(private router: Router) { }

  onSelect(id: number) {
    this.router.navigate(['/app', 'leagues', id]);
  }
  
}
