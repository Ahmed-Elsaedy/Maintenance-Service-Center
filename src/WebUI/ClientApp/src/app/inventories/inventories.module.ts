import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core.module';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SearchInventoryComponent } from './search-inventory/search-inventory.component';
import { AddTransactionComponent } from './add-transaction/add-transaction.component';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchInventoryComponent,
        pathMatch: 'full'
      }
    ]),
  ],
  declarations: [
    SearchInventoryComponent,
    AddTransactionComponent
  ],
  entryComponents:[
    SearchInventoryComponent
  ]
})
export class InventoriesModule { }
