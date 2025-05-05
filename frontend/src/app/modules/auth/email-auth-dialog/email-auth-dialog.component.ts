import { Component } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MatDialogModule, MatDialogActions } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatButtonModule }    from '@angular/material/button';
import { AuthService }        from '../../../core/auth.service';
import { Router }             from '@angular/router';

@Component({
  selector: 'app-email-auth-dialog',
  standalone: true,
  imports: [
    CommonModule,
    NgIf,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDialogActions
  ],
  templateUrl: './email-auth-dialog.component.html',
  styleUrls: ['./email-auth-dialog.component.scss']
})
export class EmailAuthDialogComponent {
  isLogin = true;
  loading = false;
  errorMsg = '';
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmailAuthDialogComponent>,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email:    ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  toggleMode() {
    this.isLogin = !this.isLogin;
    this.errorMsg = '';
  }

  submit() {
    if (this.form.invalid) return;
    this.loading = true;
    const { email, password } = this.form.value;

    const obs = this.isLogin
      ? this.auth.login(email, password)
      : this.auth.register(email, password);

    obs.subscribe({
      next: (res: any) => {
        if (this.isLogin && res.token) {
          localStorage.setItem('token', res.token);
        }
        this.dialogRef.close(true);
        this.router.navigateByUrl('/participations');
      },
      error: err => {
        this.loading = false;
        this.errorMsg = err.error?.message || 'Ha fallat';
      }
    });
  }
}
