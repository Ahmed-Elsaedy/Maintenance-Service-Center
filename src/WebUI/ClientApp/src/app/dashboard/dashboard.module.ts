import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { ChartsModule } from 'ng2-charts';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CoreModule } from '../core.module';
import { OrdersByCategoryComponent } from './orders-by-category/orders-by-category.component';
import { OrdersOverTimeComponent } from './orders-over-time/orders-over-time.component';
import { LowestSparesByStoresComponent } from './lowest-spares-by-stores/lowest-spares-by-stores.component';
import { TransactionsOverTimeComponent } from './transactions-over-time/transactions-over-time.component';
import { OrdersSmsOverTimeComponent } from './orders-sms-over-time/orders-sms-over-time.component';
import { AvailableSmsBalanceComponent } from './available-sms-balance/available-sms-balance.component';
import { TicketsByEmployeesComponent } from './tickets-by-employees/tickets-by-employees.component';



@NgModule({
  imports: [
    CommonModule,
    ChartsModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      { path: '', component: DashboardComponent }
    ])
  ],
  declarations: [
    DashboardComponent, 
    OrdersByCategoryComponent, 
    OrdersOverTimeComponent, 
    LowestSparesByStoresComponent,
    TransactionsOverTimeComponent,
    OrdersSmsOverTimeComponent,
    AvailableSmsBalanceComponent,
    TicketsByEmployeesComponent
  ]
})
export class DashboardModule { }
