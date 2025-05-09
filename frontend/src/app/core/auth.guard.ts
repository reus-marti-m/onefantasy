import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  UrlTree
} from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('token');
    const isGuest = localStorage.getItem('guest') === 'true';

    // Si tenim un JWT vàlid o l’usuari és convidat, permet passar
    if (token) {
      console.log("token:" + token)
      return true;
    }

    if (isGuest) {
      console.log("isGuest")
      return true;
    }

    // Altrament redirigeix a l’arrel (Welcome)
    return this.router.parseUrl('/');
  }
}
