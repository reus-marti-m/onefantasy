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
    MatNativeDateModule
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

  @ViewChild('drawer') drawer!: MatSidenav;

  constructor(private router: Router, private route: ActivatedRoute) {
    this.checkScreen();
  }

  @HostListener('window:resize')
  checkScreen() {
    this.isMobile = window.innerWidth < 768;
  }

  ngOnInit() {
    this.checkScreen();

    this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe((e: NavigationEnd) => {

        // check if we have opened a modal window
        const hasModal = this.route.children.some(r => r.outlet === 'modal');
        this.modalActive = hasModal;

        // Check if we are on mobile and have opened a league or participation
        if (this.isMobile) {
          const cleanUrl = e.urlAfterRedirects.split(';')[0];
          this.showDetail = /^\/app\/(participations|leagues)\/\d+$/.test(cleanUrl);
        }
      });
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
  this.router.navigate(['/app']);
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

  openFullScreen(panel: 'profile' | 'rules' | 'help' | 'settings' | 'create-league' | 'preferences') {
    this.drawer.close();
    this.router.navigate(
      [{ outlets: { modal: panel } }],
      { relativeTo: this.route }
    );
  }

}
