import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-league-detail',
  imports: [CommonModule],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class LeagueDetailComponent implements OnInit {
  
  id: number | null = null;
    private sub!: Subscription;
  
    constructor(private route: ActivatedRoute) { }
  
    ngOnInit() {
      this.sub = this.route.paramMap.subscribe(params => {
        const val = params.get('id');
        this.id = val !== null ? +val : null;
      });
    }
  
    ngOnDestroy() {
      this.sub.unsubscribe();
    }
    
}
