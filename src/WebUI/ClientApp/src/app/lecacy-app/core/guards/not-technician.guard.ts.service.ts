import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeNotTechnicianGuard implements CanActivate {
  constructor(private authorize: AuthorizeService, private router: Router) {
  }
  canActivate() {
    return this.authorize.getUser().pipe(map((res: any) => {
      var result = res && !res.roles.includes('Technician');
      return result;
    }));
  }
}