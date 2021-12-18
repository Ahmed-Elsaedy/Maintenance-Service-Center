import { ErrorHandler, Injector, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class ErrorService implements ErrorHandler {
    constructor(private injector: Injector) { }
    handleError(error: any) {
        console.log(error);
        // debugger;
        // const router = this.injector.get(Router);
        // if (Error instanceof HttpErrorResponse) {
        //     console.log(error.status);
        // }
        // else {
        //     console.error("an error occurred here broo");
        // }
       // router.navigate(['error']);
       //alert('sdf');
    }
}
