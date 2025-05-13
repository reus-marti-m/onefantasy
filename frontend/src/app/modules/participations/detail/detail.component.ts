import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { TripleToggleComponent } from './form-components/triple-toggle/triple-toggle.component';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ChipSelectorComponent } from './chip-selector/chip-selector.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {
  MinigameMatchDtoResponse, MinigamePlayersDtoResponse, MinigameResultDtoResponse, MinigameScoresDtoResponse, ParticipationDtoResponse,
  ParticipationExtraDtoResponse, ParticipationSpecialDtoResponse, ParticipationStandardDtoResponse, Service
} from '../../../core/api';

export interface ToggleOption {
  label: string;
  value: any;
  info?: string;
  result?: 'success' | 'error';
}

export interface TripleToggleModel {
  options: ToggleOption[];
  selected: string[];
  actualResult: string;
  disabled: boolean;
}

export interface ChipSelectorModel {
  title: string;
  options: ToggleOption[];
  selected: string[];
  actualResult: string[];
  disabled: boolean;
}

export type MinijocItem =
  | { kind: 'triple'; model: TripleToggleModel }
  | { kind: 'chip'; model: ChipSelectorModel };

export interface MinijocGroup {
  title: string;
  items: MinijocItem[];
  score: number | undefined;
}

@Component({
  selector: 'app-participation-detail',
  templateUrl: './detail.component.html',
  standalone: true,
  imports: [
    CommonModule,
    TripleToggleComponent,
    ChipSelectorComponent,
    MatDialogModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatButtonToggleModule
  ],
  providers: [Service],
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit, OnDestroy {

  id: number | null = null;
  private season: number | null = null;
  private sub!: Subscription;
  participationStarted: boolean = false;

  minigameToggles: TripleToggleModel[] = [];
  chipModels: ChipSelectorModel[] = [];
  groups: MinijocGroup[] = [];
  participation!: ParticipationDtoResponse;

  constructor(
    private route: ActivatedRoute,
    private service: Service
  ) { }

  ngOnInit() {
    this.sub = this.route.paramMap.subscribe(params => {
      const val = params.get('id');
      this.id = val !== null ? +val : null;
      this.season = 1;
      this.initParticipation();
    });
  }

  ngOnDestroy() {
    this.minigameToggles = [];
    this.chipModels = [];
    this.sub.unsubscribe();
  }

  private initParticipation() {
    if (this.season == null || this.id == null) {
      return;
    }

    this.service
      .participations(this.season, this.id)
      .subscribe({
        next: (resp: ParticipationDtoResponse) => {
          this.participationStarted = new Date() > resp.date;
          console.log(resp);
          switch (resp.type) {
            case 1:
              this.handleExtra(resp as ParticipationExtraDtoResponse);
              break;
            case 2:
              this.handleSpecial(resp as ParticipationSpecialDtoResponse);
              break;
            default:
              this.handleStandard(resp as ParticipationStandardDtoResponse);
          }
        },
        error: err => {
          console.error('Failed to load participation, using mock', err);
        }
      });
  }

  private handleStandard(p: ParticipationStandardDtoResponse) {
    this.participation = p;
    this.groups = [];

    // Multi
    const multiItems: MinijocItem[] = [
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match1) },
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match2) },
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match3) },
    ];
    this.groups.push({ title: 'Multi', items: multiItems, score: p.minigameGroupMulti.score });

    // Match
    const g3 = p.minigameGroupMatch3;
    const title3 = `Local${g3.homeTeamId} v Visitant${g3.visitingTeamId}`;
    const match3Items: MinijocItem[] = [
      { kind: 'chip', model: this.buildChipsModelForScores(g3.minigameScores) },
      { kind: 'chip', model: this.buildChipsModelForPlayers(g3.minigamePlayers1) },
      { kind: 'chip', model: this.buildChipsModelForPlayers(g3.minigamePlayers2) },
    ];
    this.groups.push({ title: title3, items: match3Items, score: g3.score });
  }

  private handleExtra(p: ParticipationExtraDtoResponse) {
    this.participation = p;
    this.groups = [];

    // Match 1
    const ga = p.minigameGroupMatch2A;
    const titleA = `Local${ga.homeTeamId} v Visitant${ga.visitingTeamId}`;
    this.groups.push({
      title: titleA,
      items: [
        { kind: 'chip', model: this.buildChipsModelForScores(ga.minigameScores) },
        { kind: 'chip', model: this.buildChipsModelForPlayers(ga.minigamePlayers) },
      ],
      score: ga.score
    });

    // Match 2
    const gb = p.minigameGroupMatch2B;
    const titleB = `Local${gb.homeTeamId} v Visitant${gb.visitingTeamId}`;
    this.groups.push({
      title: titleB,
      items: [
        { kind: 'triple', model: this.buildTripleModelForMatch(gb.minigameMatch) },
        { kind: 'chip', model: this.buildChipsModelForPlayers(gb.minigamePlayers) },
      ],
      score: gb.score
    });
  }

  private handleSpecial(p: ParticipationSpecialDtoResponse) {
    this.handleExtra(p);
  }

  private buildTripleModelForResult(mg: MinigameResultDtoResponse): TripleToggleModel {
    const opts: ToggleOption[] = [
      {
        label: `Local${mg.homeVictory.teamId}`,
        value: `home_${mg.homeVictory.id ?? ''}`,
        info: this.makeInfo(mg.homeVictory.price ?? 100, true, mg.isResolved, mg.homeVictory.hasOccurred, mg.homeVictory.isPlayed)[0]
      },
      {
        label: 'Empat',
        value: `draw_${mg.draw.id ?? ''}`,
        info: this.makeInfo(mg.draw.price ?? 100, true, mg.isResolved, mg.draw.hasOccurred, mg.draw.isPlayed)[0]
      },
      {
        label: `Visitant${mg.visitingVictory.teamId}`,
        value: `away_${mg.visitingVictory.id ?? ''}`,
        info: this.makeInfo(mg.visitingVictory.price ?? 100, true, mg.isResolved, mg.visitingVictory.hasOccurred, mg.visitingVictory.isPlayed)[0]
      }
    ];

    const selected = [
      ...(mg.homeVictory.isPlayed ? [opts[0].value] : []),
      ...(mg.draw.isPlayed ? [opts[1].value] : []),
      ...(mg.visitingVictory.isPlayed ? [opts[2].value] : [])
    ];

    const actual = mg.homeVictory.hasOccurred
      ? opts[0].value
      : mg.draw.hasOccurred
        ? opts[1].value
        : mg.visitingVictory.hasOccurred
          ? opts[2].value
          : '';

    return {
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.participationStarted
    };
  }

  private buildTripleModelForMatch(mg: MinigameMatchDtoResponse): TripleToggleModel {
    let first = true;
    const opts: ToggleOption[] = mg.options!.map(opt => {
      let label: string;

      if (opt.min == null) {
        label = `Menys de ${(opt.max ?? 0) + 1}`;
      }
      else if (opt.max == null) {
        label = `Més de ${(opt.min ?? 0) - 1}`;
      }
      else {
        label = `${opt.min}`;
      }

      let info = this.makeInfo(opt.price ?? 100, first, mg.isResolved, opt.hasOccurred, opt.isPlayed);
      first = info[1];

      return {
        label,
        value: `interval_${opt.id ?? ''}`,
        info: info[0]
      };
    });

    const selected = mg.options!
      .map((opt, i) => opt.isPlayed ? opts[i].value : null)
      .filter(v => v != null) as string[];

    const actual = mg.options!
      .map((opt, i) => opt.hasOccurred ? opts[i].value : null)
      .find(v => v != null) || '';

    return {
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.participationStarted
    };
  }

  private buildChipsModelForScores(mg: MinigameScoresDtoResponse): ChipSelectorModel {
    let first = true;
    const opts: ToggleOption[] = mg.options!.map(opt => {
      let info = this.makeInfo(opt.price ?? 100, first, mg.isResolved, opt.hasOccurred, opt.isPlayed)
      first = info[1];
      return {
        label: `${opt.homeGoals} - ${opt.awayGoals}`,
        value: `score_${opt.id ?? ''}`,
        info: info[0]
      }
    });

    const selected = mg.options!
      .map((opt, i) => opt.isPlayed ? opts[i].value : null)
      .filter(v => v != null) as string[];

    const actual = mg.options!
      .map((opt, i) => opt.hasOccurred ? opts[i].value : null)
      .filter(v => v != null) as string[];

    return {
      title: 'Resultat exacte',
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.participationStarted
    };
  }

  private buildChipsModelForPlayers(mg: MinigamePlayersDtoResponse): ChipSelectorModel {
    let first = true;
    const opts: ToggleOption[] = mg.options!.map(opt => {
      let info = this.makeInfo(opt.price ?? 100, first, mg.isResolved, opt.hasOccurred, opt.isPlayed)
      first = info[1];
      return {
        label: `${opt.playerId} (XXX)`,
        value: `player_${opt.id ?? ''}`,
        info: info[0]
      }
    });

    const selected = mg.options!
      .map((opt, i) => opt.isPlayed ? opts[i].value : null)
      .filter(v => v != null) as string[];

    const actual = mg.options!
      .map((opt, i) => opt.hasOccurred ? opts[i].value : null)
      .filter(v => v != null) as string[];

    return {
      title: 'Jugador',
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.participationStarted
    };
  }

  getGroupMaxPoints(): number {
    switch (this.participation.type) {
      case 1: return 18;
      case 2: return 36;
      default: return 27;
    }
  }

  getPerItemPoints(): number {
    return this.participation.type === 2 ? 16 : 8;
  }

  private makeInfo(price: number, firstWin: boolean, isResolved: boolean | undefined, hasOccurred: boolean | undefined, isPlayed: boolean | undefined): [string, boolean] {
    if (!this.participationStarted) {
      return [`${price} M`, true];
    }

    if (!this.participation.hasPlayed || !isResolved) {
      return ['-', true];
    }

    if (isPlayed) {
      if (hasOccurred && firstWin) {
        return [`${this.getPerItemPoints()} punts`, false];
      } else {
        return ['0 punts', true];
      }
    }

    return ['-', true];
  }

  // TODO: Comunes amb component de llista, haurien d'estar en un arxiu comú

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

  isPlayable(p: ParticipationDtoResponse) {
    return new Date(p.date).getTime() >= Date.now();
  }

  getMaxPoints(p: ParticipationDtoResponse): number {
    switch (p.type) {
      case 1: return 40;
      case 2: return 80;
      default: return 60;
    }
  }

}
