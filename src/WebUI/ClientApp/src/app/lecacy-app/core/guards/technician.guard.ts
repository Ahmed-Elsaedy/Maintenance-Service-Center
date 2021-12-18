import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import { AuthorizeService, IUser } from 'src/api-authorization/authorize.service';
import { ApplicationPaths, QueryParameterNames } from 'src/api-authorization/api-authorization.constants';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeTechnicianGuard implements CanActivate {
  constructor(private authorize: AuthorizeService, private router: Router) {
  }
  canActivate() {
    return this.authorize.getUser().pipe(map((res: any) => {
      var result = res && res.roles.includes('Technician');
      return result;
    }));
  }
}


