import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private base = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}

  login(email: string, password: string) {
    return this.http.post<{ token: string }>(
      `${this.base}/login`,
      { email, password }
    );
  }

  register(email: string, password: string) {
    return this.http.post(`${this.base}/register`, { email, password });
  }
}
