import { Component, Inject, Input, OnInit, Output } from '@angular/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { MatDialog, MatDialogConfig, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { InventoriesClient, SearchStoreDto, SearchSparePartDto, CreateInventoryTransactionCommand, ValueGroupsClient, ValueGroupDto, InventoryTransactionsClient, SearchInventoryDto, StoresClient } from 'src/app/ElarabyCA-api';
import { SelectionChange, SelectionModel } from '@angular/cdk/collections';
import { InventoriesDataSource } from '../inventory.datasource';
import { ActivatedRoute, Router } from '@angular/router';
import { Direction } from '@angular/cdk/bidi/directionality';
import { AddTransactionComponent } from '../add-transaction/add-transaction.component';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';

export function getDirection() {
  return <Direction>document.querySelector('html').getAttribute('dir');
}

@Component({
  selector: 'app-search-inventory',
  templateUrl: './search-inventory.component.html',
  styleUrls: ['./search-inventory.component.css']
})
export class SearchInventoryComponent implements OnInit {
  public data: any;

  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;

  activatedStore: SearchStoreDto;
  activatedSparePart: SearchSparePartDto;

  transactionTypes: ValueGroupDto[];
  referenceTypes: ValueGroupDto[];
  stores: SearchStoreDto[];

  constructor(public dialog: MatDialog,
    private inventoriesClient: InventoriesClient,
    private transactionsClient: InventoryTransactionsClient,
    private valueGroupsClient: ValueGroupsClient,
    private activatedRoute: ActivatedRoute,
    private storesClient: StoresClient,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'equalizer',
      title: 'topMenu.inventories',
      actions: [
        { id: 1, title: 'indexView.newTransaction', icon: 'add_circle_outline', disabled: true, execute: () => this.onAddTransaction(null) },
        { id: 2, title: 'indexView.transactionsDetails', icon: 'history', disabled: true, execute: () => this.onTransactionsDetails(null) }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.store.title', name: 'storeTitle', sortable: true },
        { title: 'models.sparepart.sparePartId', name: 'spareItemId', sortable: true },
        { title: 'models.sparepart.title', name: 'sparePartTitle', sortable: true },
        { title: 'models.sparepart.barcode', name: 'sparePartBarcode', sortable: true },
        { title: 'models.inventory.openingBalance', name: 'openingBalance' },
        { title: 'models.inventory.currentBalance', name: 'currentBalance' }
      ],
      options: [
        { id: 1, title: "indexView.newTransaction", icon: "add_circle_outline", execute: (e) => this.onAddTransaction(e) },
        { id: 2, title: "indexView.balanceDetails", icon: "history", execute: (e) => this.onTransactionsDetails(e) }
      ],
      enableOptions: true,
      optionsColumnWidth: '120px',
      dataSource: new InventoriesDataSource(this.inventoriesClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };

    if (this.data) {
      this.searchGridOptions.dataSource.query.storeId = this.data.store_id || undefined;
      this.searchGridOptions.dataSource.query.sparePartId = this.data.sparepart_id || undefined;

      this.toolbarOptions.subTitle = this.data.title ? ` - ${this.data.title}` : '';
      this.searchGridOptions.dataSource.loadData();

    } else {
      this.activatedRoute.queryParams.subscribe(params => {
        var store_id = +params['store_id'];
        var sparepart_id = +params['sparepart_id'];

        this.searchGridOptions.dataSource.query.storeId = store_id || undefined;
        this.searchGridOptions.dataSource.query.sparePartId = sparepart_id || undefined;

        this.toolbarOptions.subTitle = params["title"] ? ` - ${params["title"]}` : '';
        this.searchGridOptions.dataSource.loadData();
      });
    }

    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));
    this.valueGroupsClient.getReferenceTypes().subscribe(res => this.referenceTypes = res);
    this.valueGroupsClient.getTransactionTypes().subscribe(res =>
      { debugger;
        this.transactionTypes = res; });
    this.storesClient.search(null).subscribe(res => this.stores = res.data);
  }

  selectionChanged(e: SelectionChange<any>) {
    this.toolbarOptions.actions[0].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
  }

  onAddTransaction(e) {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    config.data = new CreateInventoryTransactionCommand();
    const dialogRef = this.dialog.open(AddTransactionComponent, config);
    dialogRef.componentInstance.inventory = e || this.searchGridOptions.selectionModel.selected[0];
    dialogRef.componentInstance.transactionTypes = this.transactionTypes;
    dialogRef.componentInstance.referenceTypes = this.referenceTypes;
    dialogRef.componentInstance.stores = this.stores;

    var inventory: SearchInventoryDto = e || this.searchGridOptions.selectionModel.selected[0];
    dialogRef.componentInstance.stores = this.stores.filter(x => x.storeId != inventory.storeId);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        debugger;
        this.transactionsClient.create(result).subscribe(x => {
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }



  onTransactionsDetails(e) {
    var inventory: SearchInventoryDto = e || this.searchGridOptions.selectionModel.selected[0];
    if (this.data) {
      const url = this.router
        .serializeUrl(this.router.createUrlTree(['/transactions'],
          {
            queryParams:
            {
              "inventory_id": inventory.inventoryId,
              "store": inventory.storeTitle,
              "sparepart": inventory.sparePartTitle
            }
          }));
      window.open(url, '_blank');
    }
    else
      this.router.navigate(['/transactions'],
        {
          queryParams:
          {
            "inventory_id": inventory.inventoryId,
            "store": inventory.storeTitle,
            "sparepart": inventory.sparePartTitle
          }
        });
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

}
