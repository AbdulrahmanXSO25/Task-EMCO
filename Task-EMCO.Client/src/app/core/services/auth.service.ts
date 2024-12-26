import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AuthResponseDto, LoginRequest, UserDto } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = 'http://localhost:5149/api/v1/auth';
  private currentUserSubject = new BehaviorSubject<UserDto | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.API_URL}/login`, request)
      .pipe(
        tap(response => {
          localStorage.setItem('token', response.token);
          this.currentUserSubject.next(response.user);
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    window.location.reload();
  }

  getCurrentUser() {
    this.http.get<UserDto>(`${this.API_URL}/me`)
      .subscribe(
        user => this.currentUserSubject.next(user)
      );
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
}