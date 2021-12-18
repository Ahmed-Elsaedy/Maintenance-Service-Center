import { NgModule, Injectable } from '@angular/core';
import { RouterModule, Routes, CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { OrdersComponent } from './orders.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { SidePanelService } from '../core/services/side-panel.service';
import { EditTicketComponent } from '../tickets/edit-ticket/edit-ticket.component';
import { PatchEditComponent } from './patch-edit/patch-edit.component';
import { QueryBuilderComponent } from '../shared/query-builder/query-builder.component';
import { OrdersFilterComponent } from './orders-filter/orders-filter.component';
import { SidePanelGuard } from '../core/guards/auth-guard';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';

const routes: Routes = [
    {
        path: 'orders', component: LayoutComponent, canActivate: [AuthorizeGuard],
        children: [
            { path: 'list', component: OrdersComponent },
            { path: 'new', component: EditOrderComponent, outlet: 'side', canActivate: [SidePanelGuard] },
            { path: 'edit', component: EditOrderComponent, outlet: 'side', canActivate: [SidePanelGuard] },
            { path: 'report', component: EditTicketComponent, outlet: 'side', canActivate: [SidePanelGuard] },
            { path: 'patch', component: PatchEditComponent, outlet: 'side', canActivate: [SidePanelGuard] },
            { path: 'filter', component: OrdersFilterComponent, outlet: 'side', canActivate: [SidePanelGuard] }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrdersRoutingModule { }


