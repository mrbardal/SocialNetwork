import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchResult } from '../models/search-result.model';
import { UserToken } from '../models/user-token.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  endPoint: string;

  constructor(private http: HttpClient) {
    this.endPoint = 'https://localhost:5001/users';
  }

  register(User: User): Observable<UserToken> {
    return this.http.post<UserToken>(this.endPoint + '/register', User);
  }

  login(User: User): Observable<UserToken> {
    return this.http.post<UserToken>(this.endPoint + '/login', User);
  }

  search(name: string): Observable<SearchResult> {
    let queryParams = new HttpParams()
      .set('name', name);
    return this.http.get<SearchResult>(this.endPoint + '/search', {
      params: queryParams,
    });
  }

  isAuthenticate(): boolean {
    return localStorage.getItem('accessToken') != null;
  }

  logout(): void {
    localStorage.clear();
  }

  get userName(): string | null {
    return localStorage.getItem('userName');
  }
}
