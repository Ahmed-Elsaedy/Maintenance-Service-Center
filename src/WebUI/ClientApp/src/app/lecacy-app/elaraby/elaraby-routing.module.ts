import { NgModule } from '@angular/core';
import { LayoutComponent } from '../shared/layout/layout.component';
import { RouterModule, Routes } from '@angular/router';
import { WorkOrdersComponent } from './work-orders/work-orders.component';
import { EditOrderComponent } from '../orders/edit-order/edit-order.component';
import { SidePanelGuard } from '../core/guards/auth-guard';

const routes: Routes = [{
  path: 'elaraby', component: LayoutComponent,
  children: [
    { path: 'orders', component: WorkOrdersComponent },
    { path: 'download', component: EditOrderComponent, outlet: 'side', canActivate: [SidePanelGuard] },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ElarabyRoutingModule { }
