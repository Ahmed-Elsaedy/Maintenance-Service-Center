import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private loaderSubject: Subject<any> = new Subject<any>();
  constructor() { }

  show(id) {
    this.loaderSubject.next(id);
  }

  hide(id) {
    this.loaderSubject.next(id);
  }

  get statusChanged() {
    return this.loaderSubject.asObservable();
  }

}
