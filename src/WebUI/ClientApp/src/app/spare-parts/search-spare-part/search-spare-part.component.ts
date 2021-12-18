import { Component, OnInit } from '@angular/core';
import { Direction } from '@angular/cdk/bidi';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { SparePartsClient, CreateSparePartCommand, UpdateSparePartCommand, SearchStoreDto, StoresClient, SearchSparePartDto } from 'src/app/ElarabyCA-api';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { TranslateService } from '@ngx-translate/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';
import { SpartPartsDataSource } from '../spare-part.datasource';
import { EditSparePartComponent } from '../edit-spare-part/edit-spare-part.component';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { SparePartValidations } from '../spare-part.validations';
import { SearchInventoryComponent } from 'src/app/inventories/search-inventory/search-inventory.component';


export enum SparePartAction {
  Create = 0,
  Edit = 1,
  Delete = 2,
  Details = 3,
  Reload
}

export function getDirection() {
  return <Direction>document.querySelector('html').getAttribute('dir');
}

const StringFormat = (str: string, ...args: string[]) =>
  str.replace(/{(\d+)}/g, (match, index) => args[index] || '')

@Component({
  selector: 'app-search-spare-part',
  templateUrl: './search-spare-part.component.html',
  styleUrls: ['./search-spare-part.component.css']
})
export class SearchSparePartComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;
  stores: SearchStoreDto[];

  constructor(public dialog: MatDialog,
    private sparePartsClient: SparePartsClient,
    private storesClient: StoresClient,
    private sparePartsValidations: SparePartValidations,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'description',
      title: 'topMenu.spareParts',
      actions: [
        { id: SparePartAction.Create, title: "indexView.addNewSparepartCode", icon: "add_circle_outline", execute: () => this.onCreateStore() },
        { id: SparePartAction.Edit, title: "indexView.edit", icon: "edit", disabled: true, execute: () => this.onEditStore() },
        { id: SparePartAction.Delete, title: "indexView.delete", icon: "delete", disabled: true, execute: () => this.onDeleteStore() },
        { id: SparePartAction.Details, title: "indexView.balanceDetails", icon: "list_alt", disabled: true, execute: () => this.onSparePartDetails() },
        { id: SparePartAction.Reload, title: "", icon: "refresh", disabled: false, execute: () => this.refresh() }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.sparepart.sparePartId', name: 'sparePartId', width: '100px', sortable: true },
        { title: 'models.sparepart.title', name: 'title', sortable: true },
        { title: 'models.sparepart.barcode', name: 'barcode', sortable: true },
        { title: 'models.sparepart.totalBalance', name: 'totalBalance' },
        { title: 'models.sparepart.description', name: 'description' },
      ],
      options: [
        { id: 1, title: "indexView.balanceDetails", icon: "list_alt", execute: (e) => this.onSparePartDetails(e) }
      ],
      enableOptions: true,
      enableSearch: true,
      optionsColumnWidth: '100px',
      dataSource: new SpartPartsDataSource(this.sparePartsClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };
    this.searchGridOptions.dataSource.loadData();
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));
    this.storesClient.search(null).subscribe(x => this.stores = x.data);
  }

  selectionChanged(e: SelectionChange<any>) {
    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[3].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
  }

  onCreateStore() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    config.data = new CreateSparePartCommand();
    const dialogRef = this.dialog.open(EditSparePartComponent, config);
    dialogRef.componentInstance.stores = this.stores.filter(x => x.storeId == 1)
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.sparePartsClient.create(result).subscribe(x => {
          this.searchGridOptions.dataSource.loadData();
        }, e => this.errorHandler(e));
      }
    });
  }

  onEditStore() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    var sparePartId = this.searchGridOptions.selectionModel.selected[0].sparePartId;
    this.sparePartsClient.getById(sparePartId).subscribe(res => {
      var updateCommand: UpdateSparePartCommand = <any>res;
      config.data = updateCommand;
      const dialogRef = this.dialog.open(EditSparePartComponent, config);
      dialogRef.componentInstance.editMode = true;
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.sparePartsClient.update(sparePartId, result).subscribe(x => {
            this.searchGridOptions.dataSource.loadData();
            this.searchGridOptions.selectionModel.clear();
          }, e => this.errorHandler(e));
        }
      });
    }, e => this.errorHandler(e))
  }

  onDeleteStore() {
    // var sparePart = this.searchGridOptions.selectionModel.selected[0];
    // var error = this.sparePartsValidations.validateIfCanDeleteSparePart(sparePart);
    // if (error){
    //   this.validationErrorHandler(error);
    //   return;
    // }

    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px"
    var sparePartId = this.searchGridOptions.selectionModel.selected[0].sparePartId;
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.delete',
      message: this.translateService.instant('messages.sparepart.confirmDelete'),
      localize: true
    };
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.sparePartsClient.delete(sparePartId).subscribe(x => {
          this.searchGridOptions.dataSource.loadData();
        }, e => this.errorHandler(e));
      }
    });
  }

  refresh() {
    this.searchGridOptions.selectionModel.clear();
    this.searchGridOptions.dataSource.loadData();
  }

  onSparePartDetails(row?) {
    var sparePart: SearchSparePartDto = row || this.searchGridOptions.selectionModel.selected[0];

    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = '800px';
    config.maxHeight = '600px'
    config.autoFocus = false;
    var data = {
      sparepart_id: sparePart.sparePartId,
      title: sparePart.title
    };
    const dialogRef = this.dialog.open(SearchInventoryComponent, config);
    dialogRef.componentInstance.data = data;
    dialogRef.afterClosed().subscribe(res => { });

    return;

    this.router.navigate(['/inventories'],
      { queryParams: { "sparepart_id": sparePart.sparePartId, "title": sparePart.title } });
  }

  errorHandler(error) {
    var response = JSON.parse(error.response);
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "500px"
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.error',
      message: this.translateService.instant(response.title),
      localize: true,
      isNotification: true
    };
  }

  validationErrorHandler(message) {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "500px"
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.error',
      message: this.translateService.instant(message),
      localize: true,
      isNotification: true
    };
  }

}
