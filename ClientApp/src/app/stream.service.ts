import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { User } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class StreamService {
  constructor(
    private http: HttpClient, 
    @Inject('API_URL') private apiUrl
  ) { }

  startStream(stream: Stream) {
    return this.http.post<Stream>(`${this.apiUrl}/streams`, stream)
      .pipe(
        tap(() => console.debug('Inside startStream()')),
        catchError(this.handleError<Stream>('startStream'))
      ).toPromise();
  }

  getStreams(): Observable<Stream[]> {
    return this.http.get<Stream[]>(`${this.apiUrl}/streams`)
      .pipe(
        tap(() => console.debug('Inside getStreams()')),
        catchError(this.handleError<Stream[]>('getStreams'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      console.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }
}

export interface Stream {
  title: string,
  description: string,
  broadcaster: User
}