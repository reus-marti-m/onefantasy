import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import {
  ParticipationDtoResponse,
  ParticipationExtraDtoResponse,
  ParticipationSpecialDtoResponse,
  ParticipationStandardDtoResponse,
  Service
} from '../../../core/api';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatRippleModule } from '@angular/material/core';
import { RefreshService } from '../../../core/refresh.service';
import { Subscription } from 'rxjs';

interface ParticipationVM {
  id?: number;
  competition: string;
  date: Date;
  playable: boolean;
  starClass: string;
  leftIcon: string;
  leftClass: string;
  statusText: string;
  rightText: string;
  roundText: string;
}

@Component({
  selector: 'app-participations-list',
  standalone: true,
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule,
    MatRippleModule
  ],
  templateUrl: './list.component.html',
  styleUrls: ['../../common/list.component.scss']
})
export class ListComponent implements OnInit {

  playableVMs: ParticipationVM[] = [];
  startedVMs: ParticipationVM[] = [];

  private subs = new Subscription();

  constructor(
    private service: Service,
    private router: Router,
    private refreshSvc: RefreshService
  ) { }

  ngOnInit(): void {
    this.loadAll();
    this.subs.add(
      this.refreshSvc.refreshNeeded$.subscribe(() => this.loadAll())
    );
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  loadAll() {
    this.service.participationsAll(1).subscribe(data => {
      const now = Date.now();
      const vms = data.map(p => this.createViewModel(p, now));
      this.playableVMs = vms.filter(vm => vm.playable);
      this.startedVMs = vms.filter(vm => !vm.playable);
    });
  }

  private createViewModel(p: ParticipationDtoResponse, now: number): ParticipationVM {
    const dateObj = new Date(p.date);
    const playable = dateObj.getTime() >= now;

    // Left icon
    let leftIcon: string;
    if (playable) {
      leftIcon = p.hasPlayed ? 'check_box' : 'warning';
    } else {
      leftIcon = !p.hasPlayed ? 'close' : 'fiber_manual_record';
    }

    // CSS left icon
    let leftClass: string;
    if (playable) {
      leftClass = p.hasPlayed ? 'status-played' : 'status-pending';
    } else {
      if (!p.hasPlayed) {
        leftClass = 'status-not-sent';
      } else {
        const resolved = this.computeNotLive(p);
        leftClass = resolved ? 'status-played' : 'status-live blink';
      }
    }

    // Status text
    let statusText: string;
    if (playable) {
      statusText = p.hasPlayed ? 'Jugat' : 'Pendent';
    } else {
      if (!p.hasPlayed) {
        statusText = 'No enviat';
      } else {
        const score = p.score ?? 0;
        const max = this.getMaxPoints(p);
        statusText = `${score}/${max} punts`;
      }
    }

    // Right text
    let rightText: string;
    const nowDate = new Date();
    if (playable) {
      const diffMin = Math.round((dateObj.getTime() - nowDate.getTime()) / 60000);
      if (Math.abs(diffMin) >= 60) {
        const h = Math.floor(diffMin / 60);
        rightText = `En ${h}h`;
      } else {
        rightText = `En ${diffMin}min`;
      }
    } else {
      const utc1 = Date.UTC(dateObj.getUTCFullYear(), dateObj.getUTCMonth(), dateObj.getUTCDate());
      const utc2 = Date.UTC(nowDate.getUTCFullYear(), nowDate.getUTCMonth(), nowDate.getUTCDate());
      const days = Math.floor((utc2 - utc1) / (1000 * 60 * 60 * 24));
      if (days === 0) rightText = 'Avui';
      else if (days === 1) rightText = 'Ahir';
      else {
        const dd = String(dateObj.getDate()).padStart(2, '0');
        const mm = String(dateObj.getMonth() + 1).padStart(2, '0');
        const yyyy = dateObj.getFullYear();
        rightText = `${dd}/${mm}/${yyyy}`;
      }
    }

    // Star CSS
    let starClass: string;
    switch (p.type) {
      case 1: starClass = 'star-extra'; break;
      case 2: starClass = 'star-platinum'; break;
      default: starClass = 'star-standard';
    }

    const roundText: string = `${p.roundAbbreviation}-${p.numberInRound}`;

    return {
      id: p.id,
      competition: p.competition!,
      date: dateObj,
      playable,
      starClass,
      leftIcon,
      leftClass,
      statusText,
      rightText,
      roundText
    };
  }

  private computeNotLive(p: ParticipationDtoResponse): boolean | undefined {
    if (p.type === 1 || p.type === 2) {
      const e = p as ParticipationExtraDtoResponse | ParticipationSpecialDtoResponse;
      return e.minigameGroupMatch2A.minigamePlayers.isResolved &&
        e.minigameGroupMatch2A.minigameScores.isResolved &&
        e.minigameGroupMatch2B.minigameMatch.isResolved &&
        e.minigameGroupMatch2B.minigamePlayers.isResolved;
    } else {
      const s = p as ParticipationStandardDtoResponse;
      return s.minigameGroupMatch3.minigamePlayers1.isResolved &&
        s.minigameGroupMatch3.minigamePlayers2.isResolved &&
        s.minigameGroupMatch3.minigameScores.isResolved &&
        s.minigameGroupMulti.match1.isResolved &&
        s.minigameGroupMulti.match2.isResolved &&
        s.minigameGroupMulti.match3.isResolved;
    }
  }

  private getMaxPoints(p: ParticipationDtoResponse): number {
    switch (p.type) {
      case 1: return 40;
      case 2: return 80;
      default: return 60;
    }
  }

  onSelect(id?: number) {
    if (id) this.router.navigate(['/app', 'participations', id]);
  }
  
}
