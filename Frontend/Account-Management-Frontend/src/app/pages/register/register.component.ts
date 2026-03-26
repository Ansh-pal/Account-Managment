import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.html'
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
      next: () => {
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