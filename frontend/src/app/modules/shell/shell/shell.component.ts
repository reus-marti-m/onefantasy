import { Component, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent as ParticipationsListComponent } from '../../participations/list/list.component';
import { PublicListComponent } from '../../leagues/public-list/public-list.component';
import { PrivateListComponent } from '../../leagues/private-list/private-list.component';
import { DetailComponent as ParticipationDetailComponent } from '../../participations/detail/detail.component';
import { LeagueDetailComponent } from '../../leagues/detail/detail.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { ProfileComponent } from '../../profile/profile/profile.component';
import { RulesComponent } from '../../rules/rules/rules.component';
import { HelpComponent } from '../../help/help/help.component';
import { SettingsComponent } from '../../settings/settings/settings.component';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { NotificationsComponent } from '../../notifications/notifications/notifications.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CreateLeagueComponent } from '../../leagues/create-league/create-league.component';
import { PreferencesComponent } from '../../preferences/preferences/preferences.component';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    CommonModule,
    ParticipationsListComponent,
    PublicListComponent,
    PrivateListComponent,
    ParticipationDetailComponent,
    LeagueDetailComponent,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    ProfileComponent,
    RulesComponent,
    HelpComponent,
    SettingsComponent,
    MatButtonModule,
    NotificationsComponent,
    MatDatepickerModule,
    MatNativeDateModule,
    CreateLeagueComponent,
    PreferencesComponent
  ],
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})
export class ShellComponent {
  currentTab: 'participations' | 'public' | 'private' = 'participations';
  showDetail = false;
  isMobile = false;
  selectedParticipationId?: number;
  selectedLeagueId?: number;
  lastSelectedType: 'participation' | 'league' | null = null;
  fullScreen: 'profile' | 'rules' | 'help' | 'settings' | 'createLeague' | 'preferences' | null = null;

  constructor(private router: Router) {
    this.checkScreen();
  }

  // Detecta canvi de grandària
  @HostListener('window:resize')
  checkScreen() {
    this.isMobile = window.innerWidth < 768;
  }

  ngOnInit() {
    this.checkScreen();
  }

  onSelectItem(id: number) {
    if (this.currentTab === 'participations') {
      this.selectedParticipationId = id;
      this.lastSelectedType = 'participation';
    } else {
      this.selectedLeagueId = id;
      this.lastSelectedType = 'league';
    }
    if (this.isMobile) this.showDetail = true;
  }

  closeDetail() {
    this.showDetail = false;
  }

  tokenPresent() {
    return !!localStorage.getItem('token');
  }

  navigateToWelcome() {
    localStorage.removeItem('guest');
    this.router.navigateByUrl('/');
  }

  logout() {
    localStorage.removeItem('token');
    this.navigateToWelcome();
  }

  openFullScreen(panel: 'profile' | 'rules' | 'help' | 'settings' | 'createLeague' | 'preferences') {
    this.fullScreen = panel;
  }

}
