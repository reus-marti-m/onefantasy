import { Routes } from '@angular/router';
import { AuthGuard } from './core/auth.guard';
import { ShellComponent } from './modules/shell/shell/shell.component';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./modules/welcome/welcome/welcome.component')
        .then(m => m.WelcomeComponent)
  },
  {
    path: 'app',
    component: ShellComponent,
    canActivate: [AuthGuard]
  },
  { path: '', redirectTo: 'app', pathMatch: 'full' },
  { path: '**', redirectTo: 'app' }
];
