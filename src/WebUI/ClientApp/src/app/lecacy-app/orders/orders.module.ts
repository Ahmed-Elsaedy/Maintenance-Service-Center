import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PatchEditComponent } from './patch-edit/patch-edit.component';
import { OrdersFilterComponent } from './orders-filter/orders-filter.component';
import { ClipboardModule } from 'ngx-clipboard';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from "@ngx-translate/core";

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,
    OrdersRoutingModule,
    ClipboardModule,
    TranslateModule
  ],
  declarations: [
    PatchEditComponent,
    EditOrderComponent,
    OrdersFilterComponent,
    OrdersComponent
  ],
  providers: [
  ]
})
export class OrdersModule { }
