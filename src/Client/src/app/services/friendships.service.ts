import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Friendship } from '../models/friendship.model';

@Injectable({
  providedIn: 'root'
})
export class FriendshipService {
  serviceUrl: string;

  constructor(private http: HttpClient) {
    this.serviceUrl = 'https://localhost:5001/friendships';
  }

  getFriendship(requester: string, addressee: string): Observable<Friendship> {
    let queryParams = new HttpParams()
      .set('requester', requester)
      .set('addressee', addressee);

    return this.http.get<Friendship>(this.serviceUrl, {
      params: queryParams,
    });
  }

  getFriendships(addressee: string): Observable<string[]> {
    let endPoint = `${this.serviceUrl}/requests`;
    let queryParams = new HttpParams()
      .set('addressee', addressee);

    return this.http.get<string[]>(endPoint, {
      params: queryParams,
    });
  }

  getFollowers(addressee: string): Observable<string[]> {
    let endPoint = `${this.serviceUrl}/followers`;
    let queryParams = new HttpParams()
      .set('addressee', addressee);

    return this.http.get<string[]>(endPoint, {
      params: queryParams,
    });
  }

  getFollowings(requester: string): Observable<string[]> {
    let endPoint = `${this.serviceUrl}/followings`;
    let queryParams = new HttpParams()
      .set('requester', requester);

    return this.http.get<string[]>(endPoint, {
      params: queryParams,
    });
  }

  addFriendship(friendShip: Friendship): Observable<any> {
    return this.http.post(this.serviceUrl, friendShip);
  }

  updateFriendship(friendShip: Friendship): Observable<any> {
    return this.http.put(this.serviceUrl, friendShip);
  }

}
