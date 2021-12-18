import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TechnicianInputComponent } from './technician-input.component';
import { AddInputComponent } from './add-input/add-input.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeTechnicianGuard } from '../core/guards/technician.guard';

const routes: Routes = [
    {
        path: 'technician/reports/:oid',
        component: AddInputComponent,
        canActivate: [AuthorizeGuard, AuthorizeTechnicianGuard]
    },
    {
        path: 'technician/reports',
        component: TechnicianInputComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TechnicianInputRoutingModule { }
