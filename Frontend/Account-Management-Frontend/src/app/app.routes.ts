import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { Dashboard } from './pages/dashboard/dashboard';
import { AccountsComponent } from './pages/accounts/accounts.component';
import { Transfer } from './pages/transfer/transfer';

import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'dashboard', component: Dashboard },
  { path: 'accounts', component: AccountsComponent, canActivate: [AuthGuard] },
  { path: 'transfer', component: Transfer, canActivate: [AuthGuard] },

  { path: '**', redirectTo: 'login' }
];