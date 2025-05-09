import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmailAuthDialogComponent } from '../../auth/email-auth-dialog/email-auth-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatDialogModule
  ],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})

export class WelcomeComponent {
  constructor(
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    const isGuest = localStorage.getItem('guest') === 'true';

    if (token || isGuest) {
      this.router.navigateByUrl('/app');
    }
  }

  openEmailAuth() {
    this.dialog.open(EmailAuthDialogComponent, {
      width: '400px'
    });
  }

  enterAsGuest() {
    localStorage.setItem('guest', 'true');
    this.router.navigateByUrl('/app');
  }
}
