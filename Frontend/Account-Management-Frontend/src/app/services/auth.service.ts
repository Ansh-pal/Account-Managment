import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5000/api/auth'; // change if needed

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
    localStorage.setItem('token', token);
  }

  // 📦 Get Token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // 🚪 Logout
  logout() {
    localStorage.removeItem('token');
  }

  // ✅ Check Login
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}