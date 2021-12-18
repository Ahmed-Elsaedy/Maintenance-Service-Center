import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CoreModule } from '../core.module';
import { RouterModule } from '@angular/router';
import { SearchFinancialTransactionComponent } from './search-financial-transaction/search-financial-transaction.component';
import { AddFinancialTransactionComponent } from './edit-financial-transaction/edit-financial-transaction.component';
import { FinancialStatisticsComponent } from './financial-statistics/financial-statistics.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchFinancialTransactionComponent,
        pathMatch: 'full'
      }
    ]),
  ],
  declarations: [
    SearchFinancialTransactionComponent,
    AddFinancialTransactionComponent,
    FinancialStatisticsComponent
  ],
  providers: [
    DatePipe
  ]
})
export class FinancialTransactionsModule { }
