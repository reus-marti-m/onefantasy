import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-league-detail',
  imports: [
    CommonModule,
    MatIconButton,
    MatIcon
  ],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class LeagueDetailComponent implements OnInit {

  id: number | null = null;
  private sub!: Subscription;
  isMobile = false;
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.checkScreen();
    this.sub = this.route.paramMap.subscribe(params => {
      const val = params.get('id');
      this.id = val !== null ? +val : null;
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  @HostListener('window:resize')
  checkScreen() {
    this.isMobile = window.innerWidth < 700;
  }

  closeDetail() {
    this.router.navigate(['/app']);
  }

}
