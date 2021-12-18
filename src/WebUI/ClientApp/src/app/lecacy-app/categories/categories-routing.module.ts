import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { CategoriesComponent } from './categories.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { SidePanelGuard } from '../core/guards/auth-guard';

const routes: Routes = [{
  path: 'categories', component: LayoutComponent,
  children: [
    { path: 'list', component: CategoriesComponent },
    { path: 'new', component: EditCategoryComponent, outlet: 'side', canActivate: [] },
    { path: ':id/edit', component: EditCategoryComponent, outlet: 'side', canActivate: [] }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
