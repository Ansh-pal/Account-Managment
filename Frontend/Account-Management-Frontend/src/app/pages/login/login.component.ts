import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {

  loginData = {
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: (res: any) => {
        // assuming token comes as res.token
        this.authService.saveToken(res.token);

        // redirect to dashboard
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('Login failed', err);
      }
    });
  }
}