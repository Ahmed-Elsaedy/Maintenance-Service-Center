import { Component, OnInit } from '@angular/core';
import { StoresDataSource } from '../stores.datasource';
import { Direction } from '@angular/cdk/bidi';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { StoresClient, CreateStoreCommand, EmployeeDto, EmployeesClient, UpdateStoreCommand, IUpdateStoreCommand, SearchStoreDto } from 'src/app/ElarabyCA-api';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { TranslateService } from '@ngx-translate/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { EditStoreComponent } from '../edit-store/edit-store.component';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';
import { StoresValidations } from '../stores.validations';
import { Router, NavigationExtras } from '@angular/router';
import { SearchInventoryComponent } from 'src/app/inventories/search-inventory/search-inventory.component';


export enum StoreAction {
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
  selector: 'app-search-store',
  templateUrl: './search-store.component.html',
  styleUrls: ['./search-store.component.css']
})
export class SearchStoreComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;

  employees: EmployeeDto[];

  constructor(public dialog: MatDialog,
    private storesClient: StoresClient,
    private employeesClient: EmployeesClient,
    private storesValidations: StoresValidations,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'account_balance',
      title: 'topMenu.stores',
      actions: [
        { id: StoreAction.Create, title: "indexView.create", icon: "add_circle_outline", execute: () => this.onCreateStore() },
        { id: StoreAction.Edit, title: "indexView.edit", icon: "edit", disabled: true, execute: () => this.onEditStore() },
        { id: StoreAction.Delete, title: "indexView.delete", icon: "delete", disabled: true, execute: () => this.onDeleteStore() },
        { id: StoreAction.Details, title: "indexView.balanceDetails", icon: "list_alt", disabled: true, execute: () => this.onStoreDetails() },
        { id: StoreAction.Reload, title: "", icon: "refresh", disabled: false, execute: () => this.refresh() }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.store.storeId', name: 'storeId', width: '100px', sortable: true },
        { title: 'models.store.title', name: 'title', sortable: true },
        {
          title: 'models.store.administrator', name: 'administrator',
          mapper: (o) => o.administrator?.displayName
        },
        {
          title: 'models.store.totalBalance', name: 'totalBalance',
          mapper: (o) => o.totalBalance || '-'
        },
        { title: 'models.store.description', name: 'description' },
      ],
      options: [
        { id: 1, title: "indexView.balanceDetails", icon: "list_alt", execute: (e) => this.onStoreDetails(e) }
      ],
      enableOptions: true,
      enableSearch: true,
      optionsColumnWidth: '100px',
      dataSource: new StoresDataSource(this.storesClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };
    this.searchGridOptions.dataSource.loadData();
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));

    this.employeesClient.search('', null, null, '').subscribe(x => this.employees = x.data);
  }

  selectionChanged(e: SelectionChange<any>) {
    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length == 0;
    this.toolbarOptions.actions[3].disabled = this.searchGridOptions.selectionModel.selected.length == 0;
  }

  onCreateStore() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    config.data = new CreateStoreCommand();
    const dialogRef = this.dialog.open(EditStoreComponent, config);
    dialogRef.componentInstance.employees = this.employees;
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.storesClient.create(result).subscribe(x => {
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  onEditStore() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    var storeId = this.searchGridOptions.selectionModel.selected[0].storeId;
    this.storesClient.getById(storeId).subscribe(res => {
      var updateCommand: UpdateStoreCommand = <any>res;
      updateCommand.administrator = res.administrator.oid;
      config.data = updateCommand;
      const dialogRef = this.dialog.open(EditStoreComponent, config);
      dialogRef.componentInstance.employees = this.employees;
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.storesClient.update(storeId, result).subscribe(x => {
            this.searchGridOptions.dataSource.loadData();
          }, (e) => this.errorHandler(e));
        }
      });
    }, (e) => this.errorHandler(e))
  }

  onDeleteStore() {
    var store = this.searchGridOptions.selectionModel.selected[0];
    var error = this.storesValidations.validateDeleteStore(store);
    if (error) {
      this.validationErrorHandler(error);
      return;
    }

    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px"
    var storeId = store.storeId;
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.delete',
      message: this.translateService.instant('messages.store.confirmDelete'),
      localize: true
    };
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.storesClient.delete(storeId).subscribe(x => {
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  refresh() {
    this.searchGridOptions.selectionModel.clear();
    this.searchGridOptions.dataSource.loadData();
  }

  onStoreDetails(row?) {
    var store: SearchStoreDto = row || this.searchGridOptions.selectionModel.selected[0];

    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = '800px';
    config.maxHeight = '600px'
    config.autoFocus = false;
    var data = {
      store_id: store.storeId,
      title: store.title
    };
    const dialogRef = this.dialog.open(SearchInventoryComponent, config);
    dialogRef.componentInstance.data = data;
    dialogRef.afterClosed().subscribe(res => { });

    return;

    this.router.navigate(['/inventories'],
      { queryParams: { "store_id": store.storeId, "title": store.title } });
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
