import { Component, OnInit } from '@angular/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { InventoryTransactionsClient, ValueGroupDto, ValueGroupsClient } from 'src/app/ElarabyCA-api';
import { InventoryTransactionsDataSource } from '../transactions.datasource';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-search-transaction',
  templateUrl: './search-transaction.component.html',
  styleUrls: ['./search-transaction.component.css']
})
export class SearchTransactionComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;

  constructor(public dialog: MatDialog,
    private datePipe: DatePipe,
    private activatedRoute: ActivatedRoute,
    private valueGroupsClient: ValueGroupsClient,
    private transactionsClient: InventoryTransactionsClient,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'history',
      title: 'topMenu.transactions',
      actions: [
        { id: 1, title: "indexView.edit", icon: "edit", disabled: true },
        { id: 2, title: "indexView.delete", icon: "delete", disabled: true }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.inventoryTransaction.transactionId', name: 'transactionId', sortable: true },
        {
          title: 'models.inventoryTransaction.created', name: 'created', width: '100px',
          mapper: (o) => this.datePipe.transform(o['created'], 'medium')
        },
        {
          title: 'models.inventoryTransaction.type', name: 'typeValue',
          mapper: (o) => this.translateService.instant('models.valuegroup.values.transactionType.' + o['typeValue'])
        },
        { title: 'models.store.title', name: 'storeTitle' },
        { title: 'models.sparepart.title', name: 'sparePartTitle' },
        { title: 'models.sparepart.barcode', name: 'sparePartBarcode' },
        { title: 'models.inventoryTransaction.amount', name: 'amount' },
        { title: 'models.store.administrator', name: 'storeAdministrator' },
        { title: 'models.inventoryTransaction.description', name: 'description' },
        {
          title: 'models.inventoryTransaction.referenceType', name: 'referenceTypeValue',
          mapper: (o) => {
            var refType = o['referenceTypeValue'];
            if (refType)
              return this.translateService.instant('models.valuegroup.values.referenceType.' + refType);
            else return '';
          }
        },
        { title: 'models.inventoryTransaction.referenceId', name: 'referenceId' },
      ],
      dataSource: new InventoryTransactionsDataSource(this.transactionsClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };

    this.activatedRoute.queryParams.subscribe(params => {
      var store = params['store'];
      var sparepart = params['sparepart'];

      this.searchGridOptions.dataSource.query.inventoryId = +params['inventory_id'] || undefined;
      this.toolbarOptions.subTitle = store && sparepart ? ` - ${store} - ${sparepart}` : '';


      // var hideColumns = store && sparepart;
      // this.searchGridOptions.columns[3].hidden = hideColumns;
      // this.searchGridOptions.columns[4].hidden = hideColumns;
      // this.searchGridOptions.columns[5].hidden = hideColumns;

      this.searchGridOptions.dataSource.loadData();
    });
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));
  }

  selectionChanged(e: SelectionChange<any>) {
    // this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    // this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length == 0;
  }

  onCreateStore() {
    // const config = new MatDialogConfig();
    // config.direction = getDirection();
    // config.width = "600px"
    // config.data = new CreateStoreCommand();
    // const dialogRef = this.dialog.open(EditStoreComponent, config);
    // dialogRef.componentInstance.employees = this.employees;
    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {
    //     this.storesClient.create(result).subscribe(x => {
    //       this.searchGridOptions.dataSource.loadData();
    //     }, error => this.errorHandler(error));
    //   }
    // });
  }

  onEditStore() {
    // const config = new MatDialogConfig();
    // config.direction = getDirection();
    // config.width = "600px"
    // var storeId = this.searchGridOptions.selectionModel.selected[0].storeId;
    // this.storesClient.getById(storeId).subscribe(res => {
    //   var updateCommand: UpdateStoreCommand = <any>res;
    //   updateCommand.administrator = res.administrator.oid;
    //   config.data = updateCommand;
    //   const dialogRef = this.dialog.open(EditStoreComponent, config);
    //   dialogRef.componentInstance.employees = this.employees;
    //   dialogRef.afterClosed().subscribe(result => {
    //     if (result) {
    //       this.storesClient.update(storeId, result).subscribe(x => {
    //         this.searchGridOptions.dataSource.loadData();
    //       }, this.errorHandler);
    //     }
    //   });
    // }, this.errorHandler)
  }

  onDeleteStore() {
    // const config = new MatDialogConfig();
    // config.direction = getDirection();
    // config.width = "400px"
    // var storeId = this.searchGridOptions.selectionModel.selected[0].storeId;
    // const dialogRef = this.dialog.open(MessageBoxComponent, config);
    // dialogRef.componentInstance.options = {
    //   title: 'dialogTitles.delete',
    //   message: this.translateService.instant('messages.store.confirmDelete'),
    //   localize: true
    // };
    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {
    //     this.storesClient.delete(storeId).subscribe(x => {
    //       this.searchGridOptions.dataSource.loadData();
    //     }, this.errorHandler);
    //   }
    // });
  }

  errorHandler(error) {
    console.log(error);
  }

}
