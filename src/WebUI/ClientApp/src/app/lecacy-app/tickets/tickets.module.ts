import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketsComponent } from './tickets.component';
import { SharedModule } from '../shared/shared.module';
import { TicketRoutingModule } from './tickets-routing.module';
import { EditTicketComponent } from './edit-ticket/edit-ticket.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    TicketRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    TicketsComponent,
    EditTicketComponent,
  ]
})
export class TicketsModule { }
