import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import { AuthService } from '../../../core/auth.service';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule }     from '@angular/material/input';
import { MatButtonModule }    from '@angular/material/button';
import { MatCardModule }      from '@angular/material/card';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  loading = false;
  errorMsg = '';

  constructor(
    fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.loginForm = fb.group({
      email:    ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.errorMsg = '';

    const { email, password } = this.loginForm.value;
    this.auth.login(email, password).subscribe({
      next: ({ token }) => {
        localStorage.setItem('token', token);
        this.router.navigateByUrl('/app');
      },
      error: err => {
        this.loading = false;
        this.errorMsg = err.error?.message || 'Error al fer login';
      }
    });
  }
}
