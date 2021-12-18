import { Injectable } from '@angular/core';
import { BaseService } from "./base.service";
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConfigService } from '../utils/config.service.ts';
import { catchError, map } from 'rxjs/operators';
import * as jwt_decode from 'jwt-decode';
import { JsonPipe } from '@angular/common';
const httpOptions = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

@Injectable({
    providedIn: 'root'
})
export class UserService extends BaseService {
    baseUrl: string = '';
    private _currentUser: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    get currentUser(): Observable<any> {
        return this._currentUser.asObservable();
    }

    // Observable navItem source
    // private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    // authNavStatus$ = this._authNavStatusSource.asObservable();

    //private loggedIn = false;

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();

        var auth_token = localStorage.getItem('auth_token');
        if (auth_token) {
            var decoded = jwt_decode(auth_token);
            this._currentUser.next(decoded);
        } else {
            this._currentUser.next(null);
        }
        this.baseUrl = configService.getApiURI();
    }

    register(email: string, password: string, firstName: string, lastName: string) {
        let body = JSON.stringify({ email, password, firstName, lastName });
        return this.http.post(this.baseUrl + "accounts", body, httpOptions);
    }

    login(userName, password) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http
            .post(
                this.baseUrl + 'auth/login',
                JSON.stringify({ userName, password }), httpOptions)
            .pipe(
                //map(res => res.json()),
                map((res: any) => {
                    localStorage.setItem('auth_token', res.auth_token);
                    var decoded = jwt_decode(res.auth_token);
                    this._currentUser.next(decoded);
                    return true;
                })
                //,catchError(this.handleError)
            );
    }

    logout() {
        localStorage.removeItem('auth_token');
        this._currentUser.next(null);
    }

    facebookLogin(accessToken: string) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let body = JSON.stringify({ accessToken });
        return this.http
            .post(
                this.baseUrl + 'externalauth/facebook', body, httpOptions)
            .pipe(
                //map(res => res.json())
                map((res: any) => {
                    localStorage.setItem('auth_token', res.auth_token);
                    var decoded = jwt_decode(res.auth_token);
                    this._currentUser.next(decoded);
                    return true;
                }),
                // catchError(this.handleError));
            );
    }
}