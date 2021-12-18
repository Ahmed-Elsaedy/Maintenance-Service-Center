import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { ApplicationRole, AuthorizeService } from 'src/api-authorization/authorize.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {
  constructor(private authorize: AuthorizeService,
    private router: Router) {
  }
  canActivate(_next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    var result = this.authorize.isInRoles(_next.data.roles, _next.data.matchAll);
    result.subscribe(x => {
      console.log(x);
    });
    return result;
  }
}
