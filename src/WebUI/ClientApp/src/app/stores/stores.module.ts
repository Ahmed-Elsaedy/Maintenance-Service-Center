import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core.module';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { EditStoreComponent } from './edit-store/edit-store.component';
import { SearchStoreComponent } from './search-store/search-store.component';
import { StoresValidations } from './stores.validations';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchStoreComponent,
        pathMatch: 'full'
      }
    ]),
  ],
  declarations: [
    SearchStoreComponent,
    EditStoreComponent
  ],
  providers: [
    StoresValidations
  ]
})
export class StoresModule { }
