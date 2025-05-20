import { Component, OnInit, OnDestroy, HostListener } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { TripleToggleComponent } from './form-components/triple-toggle/triple-toggle.component';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {
  CreateUserParticipationDto,
  MinigameMatchDtoResponse, MinigameMatchType, MinigamePlayersDtoResponse, MinigamePlayersType, MinigameResultDtoResponse, MinigameScoresDtoResponse,
  ParticipationDtoResponse, ParticipationExtraDtoResponse, ParticipationSpecialDtoResponse, ParticipationStandardDtoResponse, Service
} from '../../../core/api';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatRippleModule } from '@angular/material/core';
import { ChipSelectorComponent } from './form-components/chip-selector/chip-selector.component';
import { RefreshService } from '../../../core/refresh.service';

export interface ToggleOptionGroup {
  title: string,
  options: ToggleOption[];
}

export interface ToggleOption {
  label: string;
  value: any;
  info?: string;
  result?: 'success' | 'error';
  cost: number;
}

export interface TripleToggleModel {
  title: string | null;
  options: ToggleOption[];
  selected: string[];
  actualResult: [string, string][];
  disabled: boolean;
  hasResult: boolean | undefined;
  score: string | undefined;
}

export interface ChipSelectorModel {
  title: string;
  optionGroups: ToggleOptionGroup[];
  selected: string[];
  actualResult: [string, string][];
  disabled: boolean;
  hasResult: boolean | undefined;
  score: string | undefined;
}

export type MinijocItem =
  | { kind: 'triple'; model: TripleToggleModel; minigameId: number; }
  | { kind: 'chip'; model: ChipSelectorModel; minigameId: number; };

export interface MinijocGroup {
  groupId: number;
  title: string;
  items: MinijocItem[];
  score: number | undefined;
  hasResult: boolean | undefined;
  encerts: number | null;
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
    MatButtonToggleModule,
    MatTooltipModule,
    MatCardModule,
    RouterModule,
    MatRippleModule
  ],
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit, OnDestroy {

  id: number | null = null;
  private season: number | null = null;
  private sub!: Subscription;
  disabled: boolean = false;
  logged: boolean = false;
  hasChanges = false;
  hasSaved = false;
  savedAt: Date | undefined = undefined;
  isMobile = false;
  roundText = '';

  minigameToggles: TripleToggleModel[] = [];
  chipModels: ChipSelectorModel[] = [];
  groups: MinijocGroup[] = [];
  participation!: ParticipationDtoResponse;

  errors = {
    budgetExceeded: false,
    missingSelection: [] as boolean[][]
  };

  constructor(
    private route: ActivatedRoute,
    private service: Service,
    private snackBar: MatSnackBar,
    private router: Router,
    private refreshSvc: RefreshService
  ) { this.checkScreen(); }

  @HostListener('window:resize')
  checkScreen() {
    this.isMobile = window.innerWidth < 700;
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: BeforeUnloadEvent) {
    if (this.hasChanges) {
      // TODO: Revisar
      $event.returnValue = true;
    }
  }

  closeDetail() {
    this.router.navigate(['/app']);
  }

  ngOnInit() {
    this.checkScreen();
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

    this.hasChanges = false;

    this.service
      .participations(this.season, this.id)
      .subscribe({
        next: (resp: ParticipationDtoResponse) => {
          // console.log(resp);
          this.logged = this.isLogged()
          this.disabled = new Date() > resp.date || !this.logged;
          this.hasSaved = resp.hasPlayed ?? false;
          this.savedAt = resp.lastUpdate;
          this.roundText = this.getRoundText(resp);
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
          this.initValidationState();
        },
        error: err => {
          console.error('Failed to load participation, using mock', err);
        }
      });
  }

  isLogged() {
    return !localStorage.getItem('guest');
  }

  private getRoundText(p: ParticipationDtoResponse): string {
    let numberCat: string;
    switch (p.numberInRound) {
      case 1:
        numberCat = 'Primer';
        break;
      case 2:
        numberCat = 'Segon';
        break;
      case 3:
        numberCat = 'Tercer';
        break;
      case 4:
        numberCat = 'Quart';
        break;
      default:
        numberCat = `${p.numberInRound}è`
    }
    return `${numberCat} repte diari de ${p.round}. `;
  }

  private handleStandard(p: ParticipationStandardDtoResponse) {
    this.participation = p;
    this.groups = [];

    // Multi
    const multiItems: MinijocItem[] = [
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match1, 'Primer'), minigameId: p.minigameGroupMulti.match1.id! },
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match2, 'Segon'), minigameId: p.minigameGroupMulti.match2.id! },
      { kind: 'triple', model: this.buildTripleModelForResult(p.minigameGroupMulti.match3, 'Tercer'), minigameId: p.minigameGroupMulti.match3.id! },
    ];

    const multi = p.minigameGroupMulti;
    const multiMatches = [
      multi.match1,
      multi.match2,
      multi.match3
    ];
    const multiHasResult = multiMatches.every(m => m.isResolved);
    const multiEncerts = multiMatches.some(m => m.isResolved) ? multiMatches.filter(m => (m.score ?? 0) > 0).length : null;
    this.groups.push({
      title: 'Multi',
      items: multiItems,
      score: p.minigameGroupMulti.score,
      groupId: p.minigameGroupMulti.id!,
      hasResult: multiHasResult,
      encerts: multiEncerts,
    });

    // Match
    const g3 = p.minigameGroupMatch3;
    const title3 = `${g3.homeTeamName} v ${g3.visitingTeamName}`;
    const match3Items: MinijocItem[] = [
      { kind: 'chip', model: this.buildChipsModelForScores(g3.homeTeamName!, g3.visitingTeamName!, g3.minigameScores), minigameId: g3.minigameScores.id! },
      { kind: 'chip', model: this.buildChipsModelForPlayers(g3.homeTeamName!, g3.visitingTeamName!, g3.minigamePlayers1), minigameId: g3.minigamePlayers1.id! },
      { kind: 'chip', model: this.buildChipsModelForPlayers(g3.homeTeamName!, g3.visitingTeamName!, g3.minigamePlayers2), minigameId: g3.minigamePlayers2.id! },
    ];
    const scoresMatches = [
      g3.minigameScores,
      g3.minigamePlayers1,
      g3.minigamePlayers2
    ];
    const scoresHasResult = scoresMatches.every(m => m.isResolved);
    const scoresEncerts = scoresMatches.some(m => m.isResolved) ? scoresMatches.filter(m => (m.score ?? 0) > 0).length : null;
    this.groups.push({
      title: title3,
      items: match3Items,
      score: g3.score,
      groupId: g3.id!,
      hasResult: scoresHasResult,
      encerts: scoresEncerts,
    });
  }

  private handleExtra(p: ParticipationExtraDtoResponse) {
    this.participation = p;
    this.groups = [];

    // Grup A (Match 1)
    const ga = p.minigameGroupMatch2A;
    const titleA = `${ga.homeTeamName} v ${ga.visitingTeamName}`;
    const groupA = [ga.minigameScores, ga.minigamePlayers];
    const hasResultA = groupA.every(m => m.isResolved);
    const encertsA = groupA.some(m => m.isResolved) ? groupA.filter(m => (m.score ?? 0) > 0).length : null;

    this.groups.push({
      title: titleA,
      items: [
        {
          kind: 'chip',
          model: this.buildChipsModelForScores(ga.homeTeamName!, ga.visitingTeamName!, ga.minigameScores),
          minigameId: ga.minigameScores.id!
        },
        {
          kind: 'chip',
          model: this.buildChipsModelForPlayers(ga.homeTeamName!, ga.visitingTeamName!, ga.minigamePlayers),
          minigameId: ga.minigamePlayers.id!
        },
      ],
      score: ga.score,
      groupId: ga.id!,
      hasResult: hasResultA,
      encerts: encertsA,
    });

    // Grup B (Match 2)
    const gb = p.minigameGroupMatch2B;
    const titleB = `${gb.homeTeamName} v ${gb.visitingTeamName}`;
    const groupB = [gb.minigameMatch, gb.minigamePlayers];
    const hasResultB = groupB.every(m => m.isResolved);
    const encertsB = groupB.some(m => m.isResolved) ? groupB.filter(m => (m.score ?? 0) > 0).length : null;

    this.groups.push({
      title: titleB,
      items: [
        {
          kind: 'triple',
          model: this.buildTripleModelForMatch(gb.minigameMatch),
          minigameId: gb.minigameMatch.id!
        },
        {
          kind: 'chip',
          model: this.buildChipsModelForPlayers(gb.homeTeamName!, gb.visitingTeamName!, gb.minigamePlayers),
          minigameId: gb.minigamePlayers.id!
        },
      ],
      score: gb.score,
      groupId: gb.id!,
      hasResult: hasResultB,
      encerts: encertsB,
    });
  }

  private handleSpecial(p: ParticipationSpecialDtoResponse) {
    this.handleExtra(p);
  }

  private initValidationState() {
    this.errors.missingSelection = this.groups.map(g =>
      g.items.map(_ => false)
    );
    this.errors.budgetExceeded = false;
  }

  private buildTripleModelForResult(mg: MinigameResultDtoResponse, n: string): TripleToggleModel {
    const opts: ToggleOption[] = [
      {
        label: `${mg.homeVictory.teamName}`,
        value: `home_${mg.homeVictory.id ?? ''}`,
        info: this.makeInfo(mg.homeVictory.price ?? 100)[0],
        cost: mg.homeVictory.price!
      },
      {
        label: 'X',
        value: `draw_${mg.draw.id ?? ''}`,
        info: this.makeInfo(mg.draw.price ?? 100)[0],
        cost: mg.draw.price!
      },
      {
        label: `${mg.visitingVictory.teamName}`,
        value: `away_${mg.visitingVictory.id ?? ''}`,
        info: this.makeInfo(mg.visitingVictory.price ?? 100)[0],
        cost: mg.visitingVictory.price!
      }
    ];

    const selected = [
      ...(mg.homeVictory.isPlayed ? [opts[0].value] : []),
      ...(mg.draw.isPlayed ? [opts[1].value] : []),
      ...(mg.visitingVictory.isPlayed ? [opts[2].value] : [])
    ];

    const actual: [string, string][] = mg.homeVictory.hasOccurred
      ? [[opts[0].value, opts[0].label]]
      : mg.draw.hasOccurred
        ? [[opts[1].value, opts[1].label]]
        : mg.visitingVictory.hasOccurred
          ? [[opts[2].value, opts[2].label]]
          : [['', '']];

    return {
      title: `${n} resultat`,
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.disabled,
      hasResult: mg.isResolved,
      score: `${mg.score ?? 0} punts`
    };
  }

  private buildTripleModelForMatch(mg: MinigameMatchDtoResponse): TripleToggleModel {
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

      let info = this.makeInfo(opt.price ?? 100);
      return {
        label,
        value: `interval_${opt.id ?? ''}`,
        info: info[0],
        cost: opt.price!
      };
    });

    const selected = mg.options!
      .map((opt, i) => opt.isPlayed ? opts[i].value : null)
      .filter(v => v != null) as string[];

    let actual = mg.options!
      .map((opt, i) => opt.hasOccurred ? [opts[i].value, opts[i].label] : null)
      .filter(v => v != null) as [string, string][];
    if (actual.length === 0)
      actual = [['', '']];

    let title;
    switch (mg.miniGameMatchType) {
      case MinigameMatchType._1:
        title = "Targetes grogues"
        break;
      case MinigameMatchType._2:
        title = "Gols"
        break;
      default:
        title = "Número de corners"
        break;
    }

    return {
      title: title,
      options: opts,
      selected,
      actualResult: actual,
      disabled: this.disabled,
      hasResult: mg.isResolved,
      score: `${mg.score ?? 0} punts`
    };
  }

  private buildChipsModelForScores(localTeamName: string, visitingTeamName: string, mg: MinigameScoresDtoResponse): ChipSelectorModel {
    let first = true;
    const groupMap = mg.options!
      .reduce((map, opt) => {
        const [infoText, nextFirst] = this.makeInfo(opt.price ?? 100);
        first = nextFirst;

        const toggleOpt: ToggleOption = {
          label: `${opt.homeGoals} - ${opt.awayGoals}`,
          value: `score_${opt.id ?? ''}`,
          info: infoText,
          cost: opt.price!
        };

        const groupKey: string =
          opt.homeGoals > opt.awayGoals
            ? localTeamName
            : opt.homeGoals < opt.awayGoals
              ? visitingTeamName
              : 'Empat';

        if (!map.has(groupKey)) {
          map.set(groupKey, []);
        }
        map.get(groupKey)!.push(toggleOpt);

        return map;
      }, new Map<string, ToggleOption[]>());

    const scoreOptionGroups: ToggleOptionGroup[] = [
      localTeamName,
      'Empat',
      visitingTeamName
    ]
      .filter(key => groupMap.has(key))
      .map(title => {
        const options = groupMap.get(title)!;
        options.sort((a, b) => b.cost - a.cost);
        return { title, options };
      });

    const selectedScores: string[] = mg.options!
      .filter(o => o.isPlayed)
      .map(o => `score_${o.id}`);

    const actualScores: [string, string][] = mg.options!
      .filter(o => o.hasOccurred)
      .map(o => [
        `score_${o.id}`,
        `${o.homeGoals} - ${o.awayGoals}`
      ]);

    return {
      title: 'Resultat exacte',
      optionGroups: scoreOptionGroups,
      selected: selectedScores,
      actualResult: actualScores,
      disabled: this.disabled,
      hasResult: mg.isResolved,
      score: `${mg.score ?? 0} punts`
    };
  }

  private buildChipsModelForPlayers(localTeamName: string, visitingTeamName: string, mg: MinigamePlayersDtoResponse): ChipSelectorModel {

    const groupMap = mg.options!
      .reduce((map, opt) => {
        const [infoText, nextFirst] = this.makeInfo(opt.price ?? 100);

        const toggleOpt: ToggleOption = {
          label: opt.playerName!,
          value: `player_${opt.id ?? ''}`,
          info: infoText,
          cost: opt.price!
        };

        const groupKey = opt.teamName || '';
        if (!map.has(groupKey)) {
          map.set(groupKey, []);
        }
        map.get(groupKey)!.push(toggleOpt);

        return map;
      }, new Map<string, ToggleOption[]>());

    const playerOptionGroups: ToggleOptionGroup[] = [
      localTeamName,
      visitingTeamName
    ]
      .filter(key => groupMap.has(key))
      .map(title => {
        const options = groupMap.get(title)!;
        options.sort((a, b) => b.cost - a.cost);
        return { title, options };
      });

    const selectedPlayers: string[] = mg.options!
      .filter(o => o.isPlayed)
      .map(o => `player_${o.id}`);

    const actualPlayers: [string, string][] = mg.options!
      .filter(o => o.hasOccurred)
      .map(o => [
        `player_${o.id}`,
        o.playerName!
      ]);

    let title;
    switch (mg.playersType) {
      case MinigamePlayersType._1:
        title = "Assistirà";
        break;
      case MinigamePlayersType._2:
        title = "Ficarà gol o assistirà";
        break;
      case MinigamePlayersType._3:
        title = "Li treuràn groga";
        break;
      case MinigamePlayersType._4:
        title = "L'expulsaràn'";
        break;
      default:
        title = "Ficarà gol";
        break;
    }

    return {
      title: title,
      optionGroups: playerOptionGroups,
      selected: selectedPlayers,
      actualResult: actualPlayers,
      disabled: this.disabled,
      hasResult: mg.isResolved,
      score: `${mg.score ?? 0} punts`
    };
  }

  getGroupToolTipText(encerts: number | null): string {
    const m = this.getGroupMaxPoints();
    const p = this.getPerItemPoints();
    const e = this.getPerItemsExtraPoints();
    const nt = this.getPerItemMinigameNum();
    const ntText = this.getPerItemMinigameNumText();

    if (encerts === null) {
      return [
        `${m - e} pts (${nt} encerts × ${p} pts)`,
        `+ ${e} pts bonus*`,
        `= ${m} pts`,
        ``,
        `* El bonus només s'obté si encertes les ${ntText} prediccions`
      ].join('\n');
    }

    const basePoints = encerts * p;
    const bonusPoints = (encerts === nt ? e : 0);
    const totalPoints = basePoints + bonusPoints;

    return [
      `${basePoints} pts (${encerts} encert × ${p} pts)`,
      `+ ${bonusPoints} pts bonus*`,
      `= ${totalPoints} pts`,
      ``,
      `* El bonus només s'obté si encertes les ${ntText} prediccions`
    ].join('\n');
  }

  getModalityToolTipText(): string {
    const m = this.getGroupMaxPoints();
    const e = this.getPerItemsExtraPoints();
    const nt = this.getNumTextMax();
    const total = this.getMaxPoints();

    return [
      `${total - (e * 2)} pts (2 grups × ${m} pts)`,
      `+ ${e * 2} pts bonus*`,
      `= ${total} pts`,
      '',
      `* El bonus només s'obté si encertes les ${nt} prediccions`
    ].join('\n');
  }

  getMaxPoints(): number {
    switch (this.participation.type) {
      case 1: return 40;
      case 2: return 80;
      default: return 60;
    }
  }

  getGroupMaxPoints(): number {
    switch (this.participation.type) {
      case 1: return 18;
      case 2: return 36;
      default: return 27;
    }
  }

  getGroupPos(p: number): string {
    return p === 0 ? 'Primer' : 'Segon';
  }

  getPerItemPoints(): number {
    return this.participation.type === 2 ? 16 : 8;
  }

  getPerItemsExtraPoints(): number {
    switch (this.participation.type) {
      case 1:
        return 2;
      case 2:
        return 4;
      default:
        return 3;
    }
  }

  getPerItemMinigameNum(): number {
    return this.participation.type === 0 ? 3 : 2;
  }

  getPerItemMinigameNumText(): string {
    return this.participation.type === 0 ? 'tres' : 'dues';
  }

  getNumTextMax(): string {
    return this.participation.type === 0 ? 'sis' : 'quatre';
  }

  private makeInfo(price: number): [string, boolean] {
    return [`${price} M`, true];
  }

  getParticipationTypeName(p: ParticipationDtoResponse): string {
    switch (p.type) {
      case 1:
        return 'extra';
      case 2:
        return 'especial';
      default:
        return 'estàndart';
    }
  }

  getBudget(): number {
    switch (this.participation.type) {
      case 1: return 200;
      case 2: return 200;
      default: return 300;
    }
  }

  hasMissingSelection(): boolean {
    return this.errors.missingSelection
      .some(group => group.some(itemErr => itemErr));
  }

  saveParticipation() {
    const budget = this.getBudget();
    let spent = 0;

    this.errors.budgetExceeded = false;
    this.errors.missingSelection = this.groups.map(g =>
      g.items.map(() => false)
    );

    this.groups.forEach((g, gi) => {
      g.items.forEach((item, ii) => {
        const sel = item.model.selected;
        if (sel.length < 1) {
          this.errors.missingSelection[gi][ii] = true;
        }

        const optionsList: ToggleOption[] =
          item.kind === 'triple'
            ? item.model.options
            : item.model.optionGroups.flatMap(group => group.options);

        optionsList
          .filter(opt => sel.includes(opt.value))
          .forEach(opt => spent += opt.cost ?? 0);
      });
    });

    if (spent > budget) {
      this.errors.budgetExceeded = true;
    }

    if (!this.errors.budgetExceeded && !this.hasMissingSelection()) {
      const body: CreateUserParticipationDto = CreateUserParticipationDto.fromJS({
        groups: this.groups.map(g => ({
          groupId: g.groupId,
          minigames: g.items.map(item => ({
            minigameId: item.minigameId,
            selectedOptionIds: item.model.selected.map(val => {
              return Number((val as string).split('_').pop());
            })
          }))
        }))
      });

      this.service.play(1, this.id!, body).subscribe({
        next: () => {
          this.hasSaved = true;
          this.hasChanges = false;
          this.savedAt = new Date();
          this.refreshSvc.notifyRefresh();
          this.snackBar.open('Participació desada correctament.', 'Tancar', {
            duration: 3000,
            verticalPosition: 'top',
            horizontalPosition: 'center'
          });
        },
        error: ex => {
          this.snackBar.open(
            'Error enviant la participació. Torna-ho a provar.',
            'Tancar',
            {
              duration: 5000,
              verticalPosition: 'top',
              horizontalPosition: 'center'
            }
          );
        }
      });
    }
  }

  onUserChange() {
    this.hasChanges = true;
  }

  formatDate(d: Date) {
    const dd = String(d.getDate()).padStart(2, '0');
    const MM = String(d.getMonth() + 1).padStart(2, '0');
    const hh = String(d.getHours()).padStart(2, '0');
    const mm = String(d.getMinutes()).padStart(2, '0');
    return `${dd}/${MM} ${hh}:${mm}`;
  }

  getSpentBudget(): number {
    return this.groups
      .flatMap(g => g.items)
      .flatMap(item => {
        const optionsList: ToggleOption[] =
          item.kind === 'triple'
            ? item.model.options
            : item.model.optionGroups.flatMap(group => group.options);
        return optionsList
          .filter(opt => item.model.selected.includes(opt.value))
          .map(opt => opt.cost ?? 0);
      })
      .reduce((sum, cost) => sum + cost, 0);
  }

  hasNegativeSpentBudget(): boolean {
    return (this.getBudget() - this.getSpentBudget()) < 0;
  }

  getTotalMinigames(): number {
    return this.groups.reduce((sum, g) => sum + g.items.length, 0);
  }

  getSelectedCount(): number {
    return this.groups
      .flatMap(g => g.items)
      .filter(item => item.model.selected.length > 0)
      .length;
  }

  notAllMinigamesResolved(): boolean {
    let resolved = (this.getSelectedCount() - this.getTotalMinigames()) === 0
    if (resolved) {
      this.errors.missingSelection.forEach(row => row.length = 0);
    }
    return !resolved;
  }

  getResolvedCount(): number {
    return this.groups
      .flatMap(g => g.items)
      .filter(item => {
        if (item.kind === 'triple') {
          return (item.model as TripleToggleModel).actualResult.length > 0;
        } else {
          return (item.model as ChipSelectorModel).actualResult.length > 0;
        }
      })
      .length;
  }

  getLeftClass(g: MinijocGroup): string {
    if (g.hasResult)
      return 'status-played';
    else {
      return 'status-live blink';
    }
  }

  // TODO: Comunes amb component de llista, potser haurien d'estar en un arxiu comú

  navigateToWelcome() {
    this.clearLocalStorage()
    this.router.navigateByUrl('/');
  }

  clearLocalStorage() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('guest');
  }

  isLive(p: ParticipationDtoResponse): boolean {
    const participationDate = new Date(p.date).getTime();
    const nowPlus24Hours = Date.now() + 24 * 60 * 60 * 1000;
    return participationDate <= nowPlus24Hours;
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

}
