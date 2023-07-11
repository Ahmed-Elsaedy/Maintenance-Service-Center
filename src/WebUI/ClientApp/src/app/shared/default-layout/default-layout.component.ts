import { Component, OnInit, Inject, PLATFORM_ID, Optional, AfterViewInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Directionality } from '@angular/cdk/bidi';
import { Observable, of } from 'rxjs';
import { AuthorizeService, ApplicationRole } from 'src/api-authorization/authorize.service';
import { map } from 'rxjs/operators';
import { CurrentUserService } from '../current-user.service';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css'],
  providers: [CurrentUserService]
})
export class DefaultLayoutComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  langs = ['en', 'ar'];

  private sub: string;
  constructor(private translateService: TranslateService,
    public authorizeService: AuthorizeService) {
  }

  public ngOnInit(): void {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));


    // let browserlang = this.translateService.getBrowserLang();
    // browserlang = 'ar';
    // if (this.langs.indexOf(browserlang) > -1) {
    //   this.useLanguage(browserlang);
    // } else {
    this.useLanguage('ar');
    // }

    this.authorizeService.isInRoles([ApplicationRole.Administrator, ApplicationRole.DashboardManager], false)
      .subscribe(x => this.MENU_DASHBOARD = x);
    this.authorizeService.isInRoles([ApplicationRole.Administrator, ApplicationRole.WarehouseManager], false)
      .subscribe(x => this.MENU_WAREHOUSE = x);
    this.authorizeService.isInRoles([ApplicationRole.Administrator, ApplicationRole.OrdersManager], false)
      .subscribe(x => this.MENU_ORDERS = x);
    this.authorizeService.isInRoles([ApplicationRole.Administrator, ApplicationRole.SMSManager], false)
      .subscribe(x => this.MENU_SMS = x);
    this.authorizeService.isInRoles([ApplicationRole.Administrator, ApplicationRole.FinancialManager], false)
      .subscribe(x => this.MENU_FINANCIAL = x);
  }

  public useLanguage(lang: string): void {
    this.translateService.use(lang);
    if (lang == 'ar')
      document.querySelector('html').setAttribute('dir', 'rtl');
    else
      document.querySelector('html').setAttribute('dir', 'ltr');
  }


  MENU_DASHBOARD: boolean = true;
  MENU_WAREHOUSE: boolean = true;
  MENU_ORDERS: boolean = true;
  MENU_SMS: boolean = true;
  MENU_FINANCIAL: boolean = true;
}
