import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { TicketsComponent } from './tickets.component';
import { EditTicketComponent } from './edit-ticket/edit-ticket.component';
import { SidePanelGuard } from '../core/guards/auth-guard';

const routes: Routes = [{
    path: 'tickets', component: LayoutComponent,
    children: [
        { path: 'list', component: TicketsComponent },
        { path: 'new', component: EditTicketComponent, outlet: 'side', canActivate: [SidePanelGuard] },
        { path: ':id/edit', component: EditTicketComponent, outlet: 'side', canActivate: [SidePanelGuard] }
    ]
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TicketRoutingModule { }
