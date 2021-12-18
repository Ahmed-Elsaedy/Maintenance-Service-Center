import { Injectable } from '@angular/core';
import { CurrentUserClient, AuthenticationModel } from '../ElarabyCA-api';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  authenticationModel: AuthenticationModel;
  isAdminstrator: boolean;
  isWarehouseManager: boolean;
  isOrdersManager: boolean;
  isSMSManager: boolean;
  constructor(private currentUserClient: CurrentUserClient) {
    this.currentUserClient.authenticationModel().subscribe(model => {
      this.authenticationModel = model;
      this.isAdminstrator = !!this.authenticationModel?.roles.includes("Administrator");
      this.isWarehouseManager = !!this.authenticationModel?.roles.includes("WarehouseManager");
      this.isOrdersManager = !!this.authenticationModel?.roles.includes("OrdersManager");
      this.isSMSManager = !!this.authenticationModel?.roles.includes("SMSManager");
    });
  }
}
