import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = environment.authApiUrl;

  constructor(private http: HttpClient) {}

  // 🔐 Register
  register(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, data);
  }

  // 🔐 Login
  login(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, data);
  }

  // 💾 Save Token
  saveToken(token: string) {
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem('token', token);
    }
  }

  // 📦 Get Token
  getToken(): string | null {
    if (typeof localStorage === 'undefined') {
      return null;
    }

    return localStorage.getItem('token');
  }

  // 🚪 Logout
  logout() {
    if (typeof localStorage !== 'undefined') {
      localStorage.removeItem('token');
    }
  }

  // ✅ Check Login
  isLoggedIn(): boolean {
    if (typeof localStorage === 'undefined') {
      return false;
    }

    return !!localStorage.getItem('token');
  }
}