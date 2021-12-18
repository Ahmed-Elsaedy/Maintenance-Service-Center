import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable({ providedIn: "root" })
export class SidePanelService {
    private openPanelsubject = new BehaviorSubject<any>(null);
    private submitSubject = new Subject<any>();
    private closePanelSubject = new Subject();

    constructor(private router: Router, private location: Location) {
    }

    openPanel(options: { title: string, data: any }, route: any[]) {
        this.openPanelsubject.next(options);
        this.router.navigate(route, { state: { sidePanelActivated: true } });
    }

    get panelOpened(): Observable<any> {
        return this.openPanelsubject.asObservable();
    }

    submitPanel(data) {
        this.submitSubject.next(data);
    }

    get panelSubmitted() {
        return this.submitSubject.asObservable();
    }

    closePanel() {
        this.closePanelSubject.next();
        this.location.back();
    }

    get panelClosed(): Observable<any> {
        return this.closePanelSubject.asObservable();
    }
}
