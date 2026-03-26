import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  registerData = {
    name: '',
    email: '',
    password: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onRegister() {
    this.authService.register(this.registerData).subscribe({
      next: (res) => {
        console.log('Registered successfully');

        // after register → go to login
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Register failed', err);
      }
    });
  }
}