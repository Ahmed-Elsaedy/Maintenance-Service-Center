import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CoreModule } from 'src/app/core.module';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SearchSMSMessageComponent } from './search-sms-message/search-sms-message.component';
import { SMSMessageValidations } from './sms-message.validations';
import { EditSMSMessageComponent } from './edit-sms-message/edit-sms-message.component';
import { SmsMessageOrdersComponent } from './sms-message-orders/sms-message-orders.component';
import { EditOrderPhonesComponent } from './edit-order-phones/edit-order-phones.component';
import { AddSmsMessageOrdersComponent } from './add-sms-message-orders/add-sms-message-orders.component';


@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchSMSMessageComponent,
        pathMatch: 'full'
      }, {
        path: 'orders',
        component: SmsMessageOrdersComponent
      }
    ]),
  ],
  declarations: [
    SearchSMSMessageComponent,
    EditSMSMessageComponent,
    SmsMessageOrdersComponent,
    EditOrderPhonesComponent,
    AddSmsMessageOrdersComponent
  ],
  providers: [
    SMSMessageValidations,
    DatePipe
  ]
})
export class SMSMessagesModule { }
