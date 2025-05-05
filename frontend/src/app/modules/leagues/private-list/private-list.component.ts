import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-private-leagues-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './private-list.component.html',
  styleUrls: ['./private-list.component.scss']
})
export class PrivateListComponent {

  constructor(private router: Router, private route: ActivatedRoute) { }

  onSelect(id: number) {
    this.router.navigate(['/app', 'private-leagues', id]);
  }

}
