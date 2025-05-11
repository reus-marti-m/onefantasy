import { Component } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { ReactiveFormsModule, Validators, FormGroup, FormControl, NonNullableFormBuilder } from '@angular/forms';
import { MatDialogRef, MatDialogModule, MatDialogActions } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { AuthDto, Service } from '../../../core/api';

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
  providers: [
    Service,
    // { provide: API_BASE_URL, useValue: environment.apiUrl }
  ],
  templateUrl: './email-auth-dialog.component.html',
  styleUrls: ['./email-auth-dialog.component.scss']
})
export class EmailAuthDialogComponent {

  form: FormGroup<{
    email:    FormControl<string>;
    password: FormControl<string>;
  }>;

  isLogin  = true;
  loading  = false;
  errorMsg = '';

  constructor(
    private fb: NonNullableFormBuilder,
    private dialogRef: MatDialogRef<EmailAuthDialogComponent>,
    private service: Service,
    private router: Router
  ) {
    this.form = this.fb.group({
      email:    this.fb.control('', [Validators.required, Validators.email]),
      password: this.fb.control('', Validators.required)
    });
  }

  toggleMode() {
    this.isLogin = !this.isLogin;
    this.errorMsg = '';
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
      next: res => {
        if (this.isLogin && (res as any).token) {
          localStorage.setItem('token', (res as any).token);
        }
        this.dialogRef.close(true);
        this.router.navigateByUrl('/app');
      },
      error: err => {
        this.loading = false;
        this.errorMsg = err.error?.message || 'Ha fallat';
      }
    });
  }
}