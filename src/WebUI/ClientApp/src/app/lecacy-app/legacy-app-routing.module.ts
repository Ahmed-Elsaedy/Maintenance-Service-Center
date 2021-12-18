import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UnAuthorizedComponent } from './shared/un-authorized/un-authorized.component';
import { TestComponent } from './test/test.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { OrdersComponent } from './orders/orders.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { EditOrderComponent } from './orders/edit-order/edit-order.component';
import { SidePanelGuard } from './core/guards/auth-guard';
import { EditTicketComponent } from './tickets/edit-ticket/edit-ticket.component';
import { PatchEditComponent } from './orders/patch-edit/patch-edit.component';
import { OrdersFilterComponent } from './orders/orders-filter/orders-filter.component';

const routes: Routes = [
  { path: 'unauthorized', component: UnAuthorizedComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LegacyAppRoutingModule { }
