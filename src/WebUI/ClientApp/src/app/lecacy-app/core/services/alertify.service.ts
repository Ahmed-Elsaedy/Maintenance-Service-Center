import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() { }

  alert(s) {
    alertify.alert(s);
  }

  confirm(title, message, onSuccess, onCancel): void {
    alertify.confirm(title, message, onSuccess, onCancel);
  }

  notify(text, type): void {
    alertify.notify(text, type, 10, function () { });
  }
}
