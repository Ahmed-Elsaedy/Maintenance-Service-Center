import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TechnicianInputComponent } from './technician-input.component';
import { TechnicianInputRoutingModule } from './technician-input-routing.module';
import { AddInputComponent } from './add-input/add-input.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    TechnicianInputRoutingModule,
    FormsModule
    
  ],
  declarations: [TechnicianInputComponent, AddInputComponent]
})
export class TechnicianInputModule { }
