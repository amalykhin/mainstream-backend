import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, of, BehaviorSubject, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly uriPrefix = '/api';
  private userSubscription: Subscription;
  currentUser = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient) { }

  login(username, password) {
    const url = `${this.uriPrefix}/auth`;
    //console.log('Inside UserService.login()');
    const user$ = this.http.post<User>(url, { username, password })
      .pipe(
        tap(() => console.debug("Inside login()")),
        // tap((user) => this.currentUser = of(user)),
        map(user => {
          user.state = UserState[user.state]; 
          return user
        }),
        catchError(this.handleError<User>('login')),
      );
    // if (this.userSubscription) {
    //   this.userSubscription.unsubscribe();
    // };
    this.userSubscription = user$.subscribe(this.currentUser);
    return this.currentUser.toPromise();
  }

  register(newUserInfo) {
    const url = `${this.uriPrefix}/register`;
    const user$ = this.http.post<User>(url, newUserInfo)
      .pipe(
        tap(() => console.debug("Inside register()")),
        catchError(this.handleError<User>('register')),
      )
      return user$;
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }
}

export enum UserState { Offline, Active, Watching, Streaming };

export interface User {
  id: number,
  username: string,
  email: string,
  state: number | string,
  streamerKey: string,
}
