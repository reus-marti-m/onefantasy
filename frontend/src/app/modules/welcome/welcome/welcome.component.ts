import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmailAuthDialogComponent } from '../../auth/email-auth-dialog/email-auth-dialog.component';
import { Router } from '@angular/router';
import { LoginResponseDto, Service } from '../../../core/api';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})

export class WelcomeComponent {
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private service: Service
  ) { }

  ngOnInit() {
    if (localStorage.getItem('token')) {
      this.router.navigateByUrl('/app');
    }
  }

  openEmailAuth() {
    this.dialog.open(EmailAuthDialogComponent, {
      width: '400px',
      // height: '310px'
    });
  }

  enterAsGuest() {
    this.service.guest().subscribe({
      next: (res: LoginResponseDto) => {
        localStorage.setItem('token', res.token!);
        if (res.refreshToken) {
          localStorage.setItem('refreshToken', res.refreshToken);
        } else {
          localStorage.removeItem('refreshToken');
        }
        localStorage.setItem('guest', 'true');
        this.router.navigateByUrl('/app');
      },
      error: err => {
        console.error('Error fent guest login', err);
      }
    });
  }

}
