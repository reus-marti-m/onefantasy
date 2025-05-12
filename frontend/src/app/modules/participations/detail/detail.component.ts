import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ToggleOption, TripleToggleComponent } from './form-components/triple-toggle/triple-toggle.component';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ChipSelectorComponent } from './chip-selector/chip-selector.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

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
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit, OnDestroy {

  id: number | null = null;
  private sub!: Subscription;

  participationDate = new Date('2025-05-13T12:00:00');
  participationStarted = false;

  // Pel triple-toggle
  actualResult = 'draw';
  options: ToggleOption[] = [];
  userSelection: any[] = [];

  // Pel chip-selector
  actualResult2 = 'jordi_alba';
  playerOptions: ToggleOption[] = [];
  selectedPlayers: any[] = [];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.paramMap.subscribe(params => {
      const val = params.get('id');
      this.id = val !== null ? +val : null;
      this.initParticipation();
    });
  }
  ngOnDestroy() { this.sub.unsubscribe(); }

  private initParticipation() {
    this.participationStarted = new Date() > this.participationDate;

    // Triple toggle 
    this.options = [
      { label: 'Glimt', value: 'glimt', info: '40 M' },
      { label: 'Empat', value: 'draw', info: '30 M' },
      { label: 'Man Utd', value: 'lazio', info: '50 M' }
    ];

    // **Mock 40 jugadors Barça/Real**
    const barca = [
      'Lionel Messi', 'Sergio Busquets', 'Gerard Piqué', 'Jordi Alba', 'Ousmane Dembélé',
      'Pedri', 'Frenkie de Jong', 'Robert Lewandowski', 'Ansu Fati', 'Raphinha',
      'Sergi Roberto', 'Jules Koundé', 'Gavi', 'Eric García', 'Ferran Torres',
      'Ronald Araújo', 'Marcos Alonso', 'Oriol Romeu', 'Franck Kessié', 'Iñaki Peña'
    ].map(name => ({
      label: `${name} (FCB)`, value: name.toLowerCase().replace(/\s+/g, '_'),
      info: '30 M'
    }));
    const madrid = [
      'Thibaut Courtois', 'Dani Carvajal', 'Luka Modrić', 'Toni Kroos', 'Karim Benzema',
      'Vinícius Jr.', 'Eduardo Camavinga', 'Federico Valverde', 'Marco Asensio', 'Eden Hazard',
      'Dani Ceballos', 'Nacho Fernández', 'Antonio Rüdiger', 'Toni Kroos', 'Lucas Vázquez',
      'Arda Güler', 'Aurelien Tchouaméni', 'Rodrygo', 'Éder Militão', 'Théo Hernández'
    ].map(name => ({
      label: `${name} (RMA)`, value: name.toLowerCase().replace(/\s+/g, '_'),
      info: '35 M'
    }));
    this.playerOptions = [...barca, ...madrid];
    this.selectedPlayers = [
      barca[3].value,  // Jordi Alba
      madrid[5].value // Vinícius Jr.
    ];
  }

}
