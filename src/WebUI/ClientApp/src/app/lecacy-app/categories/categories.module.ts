import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories.component';
import { SharedModule } from '../shared/shared.module';
import { CategoriesRoutingModule } from './categories-routing.module';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    CategoriesRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    CategoriesComponent,
    EditCategoryComponent
  ]
})
export class CategoriesModule { }
