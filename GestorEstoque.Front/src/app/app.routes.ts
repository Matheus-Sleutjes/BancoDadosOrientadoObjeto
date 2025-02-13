import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from './services/auth.guard';

export const routes: Routes = [
    // {
    //     path: 'patient',
    //     loadChildren: async () => (await import('./pages/patient')).routes
    // },
    { path: '', redirectTo: 'login', pathMatch:"full"},
    {
      path: 'login', component: LoginComponent
    },
    {
      path: 'usuario', canActivate: [AuthGuard],
      loadChildren: async () => (await import('./pages/usuarios/routes')).routes
    }
];
