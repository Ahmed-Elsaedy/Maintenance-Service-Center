import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SideMenuComponent } from './layout/side-menu/side-menu.component';
import { ToolBarComponent } from './layout/tool-bar/tool-bar.component';
import { TopMenuComponent } from './layout/top-menu/top-menu.component';
import { RouterModule } from '@angular/router';
import { DatatableComponent } from './datatable/datatable.component';
import { QueryBuilderComponent } from './query-builder/query-builder.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ColumnFilterComponent } from './column-filter/column-filter.component';
import { SidePanelService } from '../core/services/side-panel.service';
import { LoaderService } from '../core/services/loader.service';
import { CoreModule as MatModule } from './../../core.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';

export function TranslationLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    MatModule,
    FlexLayoutModule,

    TranslateModule.forChild({
      loader: { provide: TranslateLoader, useFactory: TranslationLoaderFactory, deps: [HttpClient] }
    })
  ],
  declarations: [
    FooterComponent,
    SideMenuComponent,
    ToolBarComponent,
    TopMenuComponent,
    LayoutComponent,
    DatatableComponent,
    QueryBuilderComponent,
    ColumnFilterComponent,
  ],
  exports: [
    DatatableComponent,
    QueryBuilderComponent
  ]

})
export class SharedModule { }
