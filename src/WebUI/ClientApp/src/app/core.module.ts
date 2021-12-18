import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { BidiModule } from '@angular/cdk/bidi';
import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSpinnerOverlayComponent } from './shared/mat-spinner-overlay/mat-spinner-overlay.component';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DataGridComponent } from './shared/data-grid/data-grid.component';
import { DataToolbarComponent } from './shared/data-toolbar/data-toolbar.component';
import { MessageBoxComponent } from './shared/message-box/message-box.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

const matModules = [
    MatDatepickerModule,
    MatNativeDateModule,
    MatTabsModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatCardModule,
    MatTableModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatExpansionModule,
    MatDialogModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatAutocompleteModule
]

@NgModule({
    declarations: [
        MatSpinnerOverlayComponent,
        DataToolbarComponent,
        DataGridComponent,
        MessageBoxComponent
    ],
    imports: [
        CommonModule,
        matModules,
        FlexLayoutModule,
        TranslateModule,
        BidiModule
    ],
    exports: [
        matModules,
        TranslateModule,
        BidiModule,
        MatSpinnerOverlayComponent,
        DataGridComponent,
        DataToolbarComponent,
        MessageBoxComponent
    ],
    providers: [
        MatDatepickerModule,
        MatNativeDateModule
    ]
})
export class CoreModule { }
