import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { AlertifyService } from '../services/alertify.service';

export class ErrorInterceptor implements HttpInterceptor {
    constructor(private alertifyService: AlertifyService) {
    }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(
                retry(1),
                catchError((error: HttpErrorResponse) => {
                    let errorMessage = '';
                    if (error.error instanceof ErrorEvent) {
                        // client-side error
                        errorMessage = `Client Error: ${error.error.message}`;
                        console.log(errorMessage);
                    } else {
                        // server-side error
                        errorMessage = `Server Error Code: ${error.status}\nMessage: ${error.message}`;
                        console.log(errorMessage);
                    }
                    // window.alert(errorMessage);
                    this.alertifyService.alert(errorMessage);
                    return throwError(error);
                })
            )
    }
}