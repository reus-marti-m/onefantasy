import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { LoginResponseDto, RefreshRequestDto, Service } from '../api';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  constructor(private api: Service, private router: Router, private snackBar: MatSnackBar) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
    const authReq = token
      ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
      : req;

    return next.handle(authReq).pipe(
      catchError(err => {
        if (
          err instanceof HttpErrorResponse &&
          err.status === 401 &&
          !req.url.toLowerCase().endsWith('/auth/refresh') &&
          !req.url.toLowerCase().endsWith('/auth/login') &&
          !req.url.toLowerCase().endsWith('/auth/register') &&
          !req.url.toLowerCase().endsWith('/auth/guest')
        ) {
          const refreshToken = localStorage.getItem('refreshToken');
          if (refreshToken) {
            return this.handle401Error(authReq, next);
          } else {
            const isGuest = localStorage.getItem('guest') === 'true';
            if (!isGuest) {
              this.snackBar.open(
                'Per motius de seguretat, la sessió caduca cada 30 dies. Si us plau, torna a iniciar sessió.',
                'Tanca',
                {
                  duration: 15000,
                  verticalPosition: 'top',
                  horizontalPosition: 'center'
                }
              );
            } else {
              this.snackBar.open(
                'Per motius de seguretat, la sessió de convidat només dura 24 h. Si us plau, torna accedir amb una de les opcions d’autenticació.',
                'Tanca',
                {
                  duration: 15000,
                  verticalPosition: 'top',
                  horizontalPosition: 'center'
                }
              );
            }
            localStorage.removeItem('token');
            localStorage.removeItem('refreshToken');
            localStorage.removeItem('guest');
            this.router.navigateByUrl('/');
            return throwError(() => err);
          }
        }
        return throwError(() => err);
      })
    );
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      const accessToken = localStorage.getItem('token')!;
      const refreshToken = localStorage.getItem('refreshToken')!;
      const body: RefreshRequestDto = RefreshRequestDto.fromJS({ accessToken, refreshToken })

      return this.api.refresh(body).pipe(
        switchMap((res: LoginResponseDto) => {
          this.isRefreshing = false;
          localStorage.setItem('token', res.token!);
          localStorage.setItem('refreshToken', res.refreshToken!);
          this.refreshTokenSubject.next(res.token!);
          return next.handle(request.clone({
            setHeaders: { Authorization: `Bearer ${res.token}` }
          }));
        }),
        catchError(err => {
          this.snackBar.open(
            'Per motius de seguretat, la sessió caduca cada 30 dies. Si us plau, torna a iniciar sessió.',
            'Tanca',
            {
              duration: 15000,
              verticalPosition: 'top',
              horizontalPosition: 'center'
            }
          );
          this.isRefreshing = false;
          localStorage.removeItem('token');
          localStorage.removeItem('refreshToken');
          this.router.navigateByUrl('/');
          return throwError(() => err);
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token =>
          next.handle(request.clone({
            setHeaders: { Authorization: `Bearer ${token}` }
          }))
        )
      );
    }
  }
}
