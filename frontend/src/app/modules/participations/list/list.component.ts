import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-participations-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {

  constructor(private router: Router, private route: ActivatedRoute) { }

  onSelect(id: number) {
    this.router.navigate(['/app', 'participations', id]);
  }

}
