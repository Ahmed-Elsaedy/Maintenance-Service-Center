import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LecacyAppComponent } from './lecacy-app.component';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '@angular/flex-layout';
import { OrdersModule } from './orders/orders.module';
import { TicketsModule } from './tickets/tickets.module';
import { EmployeesModule } from './employees/employees.module';
import { CategoriesModule } from './categories/categories.module';
import { ElarabyModule } from './elaraby/elaraby.module';
import { TechnicianInputModule } from './technician-input/technician-input.module';
import { LegacyAppRoutingModule } from './legacy-app-routing.module';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { TestComponent } from './test/test.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function TranslationLoaderFactory(http: HttpClient) {
   return new TranslateHttpLoader(http);
}

@NgModule({
   imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      CoreModule,
      ApiAuthorizationModule,
      LegacyAppRoutingModule,
      //FeaturesModules\nOrdersModule,
      TicketsModule,
      EmployeesModule,
      CategoriesModule,
      ElarabyModule,
      TechnicianInputModule,
      OrdersModule,

      TranslateModule.forChild({
         loader: { provide: TranslateLoader, useFactory: TranslationLoaderFactory, deps: [HttpClient] }
      })

   ],
   declarations: [
      LecacyAppComponent,
      TestComponent
   ]
})
export class LecacyAppModule { }
