import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core.module';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SearchSparePartComponent } from './search-spare-part/search-spare-part.component';
import { EditSparePartComponent } from './edit-spare-part/edit-spare-part.component';
import { SparePartValidations } from './spare-part.validations';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    CoreModule,
    RouterModule.forChild([
      {
        path: '',
        component: SearchSparePartComponent,
        pathMatch: 'full'
      }
    ]),
  ],
  declarations: [
    SearchSparePartComponent,
    EditSparePartComponent
  ],
  providers: [
    SparePartValidations
  ]
})
export class SparePartsModule { }
