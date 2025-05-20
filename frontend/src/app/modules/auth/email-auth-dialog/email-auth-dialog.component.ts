import { Component } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { ReactiveFormsModule, Validators, FormGroup, FormControl, NonNullableFormBuilder } from '@angular/forms';
import { MatDialogRef, MatDialogModule, MatDialogActions } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthDto, LoginResponseDto, Service } from '../../../core/api';

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

  form: FormGroup<{
    email: FormControl<string>;
    password: FormControl<string>;
  }>;

  isLogin = true;
  loading = false;
  errorMsg = '';
  private passwordPattern = /^(?=.*[0-9])(?=.*[^a-zA-Z0-9])(?=.*[A-Z]).+$/;

  constructor(
    private fb: NonNullableFormBuilder,
    private dialogRef: MatDialogRef<EmailAuthDialogComponent>,
    private service: Service,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: this.fb.control('', [Validators.required, Validators.email]),
      password: this.fb.control('', [Validators.required])
    });
  }

  toggleMode() {
    this.isLogin = !this.isLogin;
    this.errorMsg = '';

    const pw = this.form.get('password')!;
    if (!this.isLogin) {
      pw.setValidators([Validators.required, Validators.pattern(this.passwordPattern)]);
    } else {
      pw.setValidators([Validators.required]);
    }
    pw.updateValueAndValidity();
  }

  submit() {
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.errorMsg = '';

    const fv = this.form.value;
    const dto = AuthDto.fromJS({ email: fv.email, password: fv.password });

    const obs = this.isLogin
      ? this.service.login(dto)
      : this.service.register(dto);

    obs.subscribe({
      next: (res: LoginResponseDto) => {
        localStorage.setItem('token', res.token!);
        localStorage.setItem('refreshToken', res.refreshToken!);
        localStorage.removeItem('guest');
        this.dialogRef.close(true);
        this.router.navigateByUrl('/app');
      },
      error: err => {
        this.loading = false;
        this.errorMsg = err.error?.message || (this.isLogin ? 'Usuari o contrasenya incorrectes.' : 'Ja existeix un compte amb aquest correu.');
      }
    });
  }
}