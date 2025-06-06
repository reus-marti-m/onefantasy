import { Component, HostListener, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent as ParticipationsListComponent } from '../../participations/list/list.component';
import { PublicListComponent } from '../../leagues/public-list/public-list.component';
import { PrivateListComponent } from '../../leagues/private-list/private-list.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { ActivatedRoute, NavigationEnd, Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { NotificationsComponent } from '../../notifications/notifications/notifications.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { filter } from 'rxjs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTabsModule } from '@angular/material/tabs';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    ParticipationsListComponent,
    PublicListComponent,
    PrivateListComponent,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    NotificationsComponent,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTooltipModule,
    MatTabsModule,
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
  modalActive = false;
  selectedTabIndex = 0;

  @ViewChild('drawer') drawer!: MatSidenav;

  constructor(private router: Router, private route: ActivatedRoute) {
    this.checkScreen();
  }

  onTabChange(idx: number) {
    const map: ('participations' | 'public' | 'private')[] = [
      'participations', 'public', 'private'
    ];
    this.currentTab = map[idx];
    this.selectedTabIndex = idx;
  }

  @HostListener('window:resize')
  checkScreen() {
    this.isMobile = window.innerWidth < 700;
  }

  navigateHome() {
    this.router.navigate(['/']);
  }

  ngOnInit() {
    this.checkScreen();

    this.updateViewFlags();

    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => {
        this.updateViewFlags();
      });
  }

  private updateViewFlags() {
    this.modalActive = this.route.children.some(r => r.outlet === 'modal');
    if (this.isMobile) {
      const cleanUrl = (this.router.url || '').split(';')[0];
      this.showDetail = /^\/app\/(participations|leagues)\/\d+$/.test(cleanUrl);
    }
  }

  clearLocalStorage() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken')
    localStorage.removeItem('guest');
  }

  isLogged() {
    return !localStorage.getItem('guest');
  }

  navigateToWelcome() {
    this.clearLocalStorage()
    this.router.navigateByUrl('/');
  }

  openFullScreen(panel: 'profile' | 'rules' | 'help' | 'settings' | 'create-league' | 'preferences') {
    this.drawer.close();
    this.router.navigate(
      [{ outlets: { modal: panel } }],
      { relativeTo: this.route }
    );
  }

}
