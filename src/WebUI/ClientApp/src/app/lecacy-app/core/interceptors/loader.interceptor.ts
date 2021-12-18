import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, pipe } from 'rxjs';
import { tap } from 'rxjs/operators';
import { LoaderService } from '../services/loader.service';
@Injectable({
    providedIn: 'root'
})
export class LoaderInterceptorService implements HttpInterceptor {
    constructor(private loaderService: LoaderService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.showLoader(req.url);
        return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {
                this.onEnd(req.url);
            }
        }, (err: any) => {
            this.onEnd(req.url);
        }));
    }
    private onEnd(url): void {
        this.hideLoader(url);
    }
    private showLoader(url): void {
        this.loaderService.show(url);
    }
    private hideLoader(url): void {
        this.loaderService.hide(url);
    }
}