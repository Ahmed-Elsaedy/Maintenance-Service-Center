import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CoreModule } from '../core.module';
import { RouterModule } from '@angular/router';
import { SearchTransactionComponent } from './search-transaction/search-transaction.component';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchTransactionComponent,
        pathMatch: 'full'
      }
    ]),
  ],
  declarations: [
    SearchTransactionComponent,
  ],
  providers: [
    DatePipe
  ]
})
export class TransactionsModule { }
