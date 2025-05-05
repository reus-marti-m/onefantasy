import { Routes } from '@angular/router';
import { WelcomeComponent } from './modules/welcome/welcome/welcome.component';
import { AuthGuard } from './core/auth.guard';
import { ShellComponent } from './modules/shell/shell/shell.component';
import { DetailComponent as ParticipationDetailComponent } from './modules/participations/detail/detail.component';
import { LeagueDetailComponent } from './modules/leagues/detail/detail.component';
import { ProfileComponent } from './modules/profile/profile/profile.component';
import { RulesComponent } from './modules/rules/rules/rules.component';
import { HelpComponent } from './modules/help/help/help.component';
import { SettingsComponent } from './modules/settings/settings/settings.component';
import { CreateLeagueComponent } from './modules/leagues/create-league/create-league.component';
import { PreferencesComponent } from './modules/preferences/preferences/preferences.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent },

  {
    path: 'app',
    component: ShellComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'participations', pathMatch: 'full' },
      {
        path: 'participations',
        children: [
          { path: ':id', component: ParticipationDetailComponent }
        ]
      },
      {
        path: 'public-leagues',
        children: [
          { path: ':id', component: LeagueDetailComponent }
        ]
      },
      {
        path: 'private-leagues',
        children: [
          { path: ':id', component: LeagueDetailComponent }
        ]
      },
      { path: 'profile', component: ProfileComponent, outlet: 'modal' },
      { path: 'rules', component: RulesComponent, outlet: 'modal' },
      { path: 'help', component: HelpComponent, outlet: 'modal' },
      { path: 'settings', component: SettingsComponent, outlet: 'modal' },
      { path: 'create-league', component: CreateLeagueComponent, outlet: 'modal' },
      { path: 'preferences', component: PreferencesComponent, outlet: 'modal' }
    ]
  },

  { path: '**', redirectTo: '' }
];
