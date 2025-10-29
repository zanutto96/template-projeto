import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _accessToken: string = '';

  constructor() {
    // Initialize token from localStorage if available
    this._accessToken = localStorage.getItem('access_token') || '';
  }

  get accessToken(): string {
    return this._accessToken;
  }

  set accessToken(token: string) {
    this._accessToken = token;
    if (token) {
      localStorage.setItem('access_token', token);
    } else {
      localStorage.removeItem('access_token');
    }
  }

  isAuthenticated(): boolean {
    return !!this._accessToken;
  }

  logout(): void {
    this._accessToken = '';
    localStorage.removeItem('access_token');
  }
}