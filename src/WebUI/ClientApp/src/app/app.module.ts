import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function TranslationLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

import { AppComponent } from './app.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { CoreModule } from './core.module';
import { LecacyAppModule } from './lecacy-app/lecacy-app.module';
import { DefaultLayoutComponent } from './shared/default-layout/default-layout.component';
import { SidePanelGuard } from './lecacy-app/core/guards/auth-guard';
import { CurrentUserService } from './shared/current-user.service';
import { AccessDeniedComponent } from './shared/access-denied/access-denied.component';
import { RoleGuardService } from './shared/role-guard.service';
import { ApplicationRole } from 'src/api-authorization/authorize.service';
import { LandPageComponent } from './shared/land-page/land-page.component';
import { LanguageTranslationModule } from './LanguageTranslationModule/LanguageTranslationModule.module';

@NgModule({
  declarations: [
    DefaultLayoutComponent,
    AccessDeniedComponent,
    AppComponent,
    LandPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    CoreModule,
    ApiAuthorizationModule,
    LanguageTranslationModule,
    RouterModule.forRoot([
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'orders/list'
      },
      {
        path: '',
        component: DefaultLayoutComponent,
        canActivate: [AuthorizeGuard],
        children: [
          {
            path: 'home',
            component: LandPageComponent
          },
          {
            path: 'dashboard',
            loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.DashboardManager] }
          },
          {
            path: '',
            loadChildren: () => import('./lecacy-app/lecacy-app.module').then(m => m.LecacyAppModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.OrdersManager] }
          },
          {
            path: 'stores',
            loadChildren: () => import('./stores/stores.module').then(m => m.StoresModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.WarehouseManager] }
          },
          {
            path: 'spareparts',
            loadChildren: () => import('./spare-parts/spare-part.module').then(m => m.SparePartsModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.WarehouseManager] }
          },
          {
            path: 'inventories',
            loadChildren: () => import('./inventories/inventories.module').then(m => m.InventoriesModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.WarehouseManager] }
          },
          {
            path: 'transactions',
            loadChildren: () => import('./transactions/transactions.module').then(m => m.TransactionsModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.WarehouseManager] }
          },
          {
            path: 'sms',
            loadChildren: () => import('./sms-messages/sms-message.module').then(m => m.SMSMessagesModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.SMSManager] }
          },
          {
            path: 'financial',
            loadChildren: () => import('./financial-transactions/financial-transactions.module').then(m => m.FinancialTransactionsModule),
            canActivate: [AuthorizeGuard, RoleGuardService],
            data: { matchAll: false, roles: [ApplicationRole.Administrator, ApplicationRole.FinancialManager] }
          },
          {
            path: '403',
            component: AccessDeniedComponent
          },
        ]
      },
      {
        path: '**', redirectTo: '/home'
      },
    ]),

    BrowserAnimationsModule,
    ModalModule.forRoot(),

    TranslateModule.forRoot({
      loader: { provide: TranslateLoader, useFactory: TranslationLoaderFactory, deps: [HttpClient] }
    })

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    // { provide: MatPaginatorIntl, useClass: MyMatPaginatorIntl}
    SidePanelGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
