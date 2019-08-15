import { Injectable } from '@angular/core';
import { Observable, throwError, of, from } from 'rxjs';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { User } from '../models/user';

@Injectable()
export class ApiService {
    private baseUrl = 'https://i1h2ug2l87.execute-api.ap-southeast-2.amazonaws.com/zombies';

    constructor(private http: HttpClient) { }

    getAllUsers(): Observable<User[]> {
        const url = this.baseUrl + '/users';
        return this.http.get<User[]>(url);
    }
    
    getUser(userId: string): Observable<User> {
        const url = this.baseUrl + '/users/' + userId;
        return this.http.get<User>(url);
    }

    addUser(user: User) : Observable<boolean> {
        const url = this.baseUrl + '/users';
        return this.http.post<boolean>(url, user);
    }

    updateUser(user: User): Observable<boolean> {
        const url = this.baseUrl + '/users/' + user.userId;
        return this.http.put<boolean>(url, user);
    }

    deleteUser(user: User): Observable<boolean> {
        const url = this.baseUrl + '/users/' + user.userId;
        return this.http.delete<boolean>(url);
    }
}