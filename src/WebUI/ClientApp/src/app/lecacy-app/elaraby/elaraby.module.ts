import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ElarabyRoutingModule } from './elaraby-routing.module';
import { WorkOrdersComponent } from './work-orders/work-orders.component';

@NgModule({
  imports: [
    CommonModule,
    ElarabyRoutingModule,
    SharedModule
  ],
  declarations: [
    WorkOrdersComponent
  ]
})
export class ElarabyModule { }
