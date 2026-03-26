import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private baseUrl = `${environment.apiUrl}/account`;

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) {}

  // 🔑 Helper for headers
  private getHeaders() {
    const token = this.authService.getToken();

    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`
      })
    };
  }

  // 📥 Get all accounts
  getAccounts(): Observable<any> {
    return this.http.get(this.baseUrl, this.getHeaders());
  }

  // ➕ Create account
  createAccount(data: any): Observable<any> {
    return this.http.post(this.baseUrl, data, this.getHeaders());
  }

  // ✏️ Update account
  updateAccount(id: number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, data, this.getHeaders());
  }

  // ❌ Delete account
  deleteAccount(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`, this.getHeaders());
  }

  // 🔍 Get single account
  getAccountById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/${id}`, this.getHeaders());
  }

  // 💸 Transfer money between accounts
  transfer(data: any): Observable<any> {
    return this.http.post('http://localhost:5000/api/transaction/transfer', data, this.getHeaders());
  }
}