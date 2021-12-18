import { Injectable, OnDestroy } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from '../services/user.service';
import { Subscription } from 'rxjs';
import { SidePanelService } from '../services/side-panel.service';

@Injectable()
export class SidePanelGuard implements CanActivate {
    constructor(private router: Router) {
    }
    canActivate() {
        var state = this.router.getCurrentNavigation().extras.state;
        if (state && state.sidePanelActivated)
            return true;
        else
            this.router.navigate(['/']);
    }
}