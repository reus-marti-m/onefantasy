import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ParticipationDtoResponse, Service } from '../../../core/api';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-participations-list',
  standalone: true,
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule
  ],
  providers: [Service],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  participations: ParticipationDtoResponse[] = [];
  playable: ParticipationDtoResponse[] = [];
  started: ParticipationDtoResponse[] = [];

  ngOnInit(): void {
    this.service.participationsAll(1).subscribe(data => {
      this.participations = data;
      this.split();
    });
  }

  constructor(private service: Service, private router: Router) { }

  private split() {
    const now = Date.now();
    this.playable = this.participations.filter(p => new Date(p.date).getTime() >= now);
    this.started = this.participations.filter(p => new Date(p.date).getTime() < now);
  }

  onSelect(id?: number) {
    if (id) this.router.navigate(['/app', 'participations', id]);
  }

  isPlayable(p: ParticipationDtoResponse) {
    return new Date(p.date).getTime() >= Date.now();
  }

  isLive(p: ParticipationDtoResponse): boolean {
    const participationDate = new Date(p.date).getTime();
    const nowPlus24Hours = Date.now() + 24 * 60 * 60 * 1000;
    return participationDate <= nowPlus24Hours;
  }

  getLeftIcon(p: ParticipationDtoResponse): string {
    if (this.isPlayable(p)) return p.hasPlayed ? 'check_box' : 'warning';
    if (!p.hasPlayed) return 'close';
    return this.isLive(p) ? 'fiber_manual_record' : 'check_circle';
  }

  getLeftClass(p: ParticipationDtoResponse): string {
    if (this.isPlayable(p)) return p.hasPlayed ? 'status-played' : 'status-pending';
    if (!p.hasPlayed) return 'status-not-sent';
    return this.isLive(p) ? 'status-live blink' : 'status-sent';
  }

  getStatusText(p: ParticipationDtoResponse): string {
    if (this.isPlayable(p)) return p.hasPlayed ? 'Enviada' : 'Pendent';
    if (!p.hasPlayed) return 'No enviada';
    const score = p.score ?? 0;
    const max = this.getMaxPoints(p);
    return `${score}/${max} punts`;
  }

  getMaxPoints(p: ParticipationDtoResponse): number {
    switch (p.type) {
      case 1: return 40;
      case 2: return 80;
      default: return 60;
    }
  }

  getRightText(p: ParticipationDtoResponse): string {
    const dt = new Date(p.date);
    const now = new Date();

    if (this.isPlayable(p)) {
      const diffMin = Math.round((dt.getTime() - now.getTime()) / 60000);
      if (Math.abs(diffMin) >= 60) {
        const h = Math.floor(diffMin / 60);
        return `En ${h}h`;
      }
      return `En ${diffMin}min`;
    }

    const utc1 = Date.UTC(dt.getUTCFullYear(), dt.getUTCMonth(), dt.getUTCDate());
    const utc2 = Date.UTC(now.getUTCFullYear(), now.getUTCMonth(), now.getUTCDate());
    const days = Math.floor((utc2 - utc1) / (1000 * 60 * 60 * 24));
    if (days === 0) return 'Avui';
    if (days === 1) return 'Ahir';
    const dd = String(dt.getDate()).padStart(2, '0');
    const mm = String(dt.getMonth() + 1).padStart(2, '0');
    const yyyy = dt.getFullYear();
    return `${dd}/${mm}/${yyyy}`;
  }

  getStarClass(p: ParticipationDtoResponse): string {
    switch (p.type) {
      case 1:
        return 'star-extra';
      case 2:
        return 'star-platinum';
      default:
        return 'star-standard';
    }
  }

}
