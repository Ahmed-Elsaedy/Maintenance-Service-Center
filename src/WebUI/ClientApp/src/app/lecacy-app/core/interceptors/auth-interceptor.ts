import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {
    constructor(private router: Router) {
    }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('authToken');
        if (token) {
            request = request.clone({
                setHeaders: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                }
            });
        }
        return next.handle(request);
    }
}