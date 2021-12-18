import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolbarService } from './services/toolbar.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppActionsService } from './services/app-actions.service';
import { DataApiService } from './services/data-api.service';
import { AlertifyService } from './services/alertify.service';
import { UserService } from './services/user.service';
import { ConfigService } from './utils/config.service.ts';
import { SidePanelGuard } from './guards/auth-guard';
import { ErrorService } from './services/error.service';
import { AuthInterceptorService } from './interceptors/auth-interceptor';
import { LoaderInterceptorService } from './interceptors/loader.interceptor';
import { SidePanelService } from './services/side-panel.service';
import { LoaderService } from './services/loader.service';
import { AuthorizeTechnicianGuard } from './guards/technician.guard';
import { AuthorizeNotTechnicianGuard } from './guards/not-technician.guard.ts.service';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  declarations: [],
  providers: [
    ToolbarService,
    DataApiService,
    AppActionsService,
    AlertifyService,
    UserService,
    ConfigService,
    SidePanelGuard,
    SidePanelService,
    LoaderService,
    AuthorizeTechnicianGuard,
    AuthorizeNotTechnicianGuard,
    //{ provide: ErrorHandler, useClass: ErrorService },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptorService, multi: true },
  ]
})
export class CoreModule { }
