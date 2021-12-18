import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeesComponent } from './employees.component';
import { EmployeesRoutingModule } from './employees-routing.module';
import { SharedModule } from '../shared/shared.module';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    EmployeesRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    EmployeesComponent,
    EditEmployeeComponent
  ]
})
export class EmployeesModule { }
