import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { EmployeesComponent } from './employees.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { SidePanelGuard } from '../core/guards/auth-guard';

const routes: Routes = [{
    path: 'employees', component: LayoutComponent,
    children: [
      { path: 'list', component: EmployeesComponent },
      { path: 'new', component: EditEmployeeComponent, outlet: 'side', canActivate: [SidePanelGuard] },
      { path: ':id/edit', component: EditEmployeeComponent, outlet: 'side', canActivate: [SidePanelGuard] }
    ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeesRoutingModule { }
