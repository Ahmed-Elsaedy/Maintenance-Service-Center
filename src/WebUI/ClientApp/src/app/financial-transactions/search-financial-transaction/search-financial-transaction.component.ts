import { Component, OnInit } from '@angular/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { getDirection, IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { CreateFinancialTransactionCommand, EmployeeDto, EmployeesClient, ExportFinancialTransactionQuery, FinancialTransaction, FinancialTransactionsClient, FinancialTransactionsStatisticsQuery, SearchFinancialTransactionDto, UpdateFinancialTransactionCommand, ValueGroupDto, ValueGroupsClient } from 'src/app/ElarabyCA-api';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { FinancialTransactionsDataSource } from '../financial-transactions.datasource';
import { AddFinancialTransactionComponent } from '../edit-financial-transaction/edit-financial-transaction.component';
import { FinancialStatisticsComponent } from '../financial-statistics/financial-statistics.component';
import { Direction } from '@angular/cdk/bidi/directionality';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-search-financial-transaction',
  templateUrl: './search-financial-transaction.component.html',
  styleUrls: ['./search-financial-transaction.component.css']
})
export class SearchFinancialTransactionComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;
  employees: EmployeeDto[];
  transactionTypes: ValueGroupDto[];
  dir: Direction;

  fromDate: Date;
  toDate: Date;

  constructor(public dialog: MatDialog,
    private datePipe: DatePipe,
    private activatedRoute: ActivatedRoute,
    private valueGroupsClient: ValueGroupsClient,
    private employeesClient: EmployeesClient,
    private transactionsClient: FinancialTransactionsClient,
    private translateService: TranslateService) { }

  ngOnInit() {
    var date = new Date(), y = date.getFullYear(), m = date.getMonth();
    this.fromDate = new Date(y, m, 1);
    this.toDate = new Date(y, m + 1, 0);

    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.toolbarOptions = {
      icon: 'money',
      title: 'topMenu.financialTransactions',
      actions: [
        { id: 1, title: "indexView.create", icon: "add_circle_outline", execute: () => this.onCreateTranasaction() },
        { id: 2, title: "indexView.edit", icon: "edit", disabled: true, execute: () => this.onEditTransaction() },
        { id: 3, title: "indexView.delete", icon: "delete", disabled: true, execute: () => this.onDeleteStore() },
        { id: 4, title: "indexView.financialStatistics", icon: "bar_chart", execute: () => this.onFinancialStatistics() },
        { id: 5, title: "indexView.refresh", icon: "refresh", execute: () => this.loadData() },
        { id: 6, title: "indexView.exportIncome", icon: "import_export", execute: () => this.exportData(false, true) },
        { id: 7, title: "indexView.exportExpense", icon: "import_export", execute: () => this.exportData(true, false) },
        { id: 8, title: "indexView.export", icon: "import_export", execute: () => this.exportData(true, true) }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.financialTransaction.transactionId', name: 'transactionId', sortable: true, width: '70px' },
        {
          title: 'models.financialTransaction.date', name: 'date', width: '100px',
          mapper: (o) => this.datePipe.transform(o['date'], 'medium')
        },
        {
          title: 'models.financialTransaction.type', name: 'type', width: '100px',
          mapper: (o) => this.translateService.instant("models.valuegroup.values.financialTransactionType." + o['typeValue'])
        },
        { title: 'models.financialTransaction.title', name: 'title' },
        { title: 'models.financialTransaction.amount', name: 'amount' },
        { title: 'models.financialTransaction.employee', name: 'employeeName' },
        { title: 'models.financialTransaction.remarks', name: 'remarks' }
      ],
      dataSource: new FinancialTransactionsDataSource(this.transactionsClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };

    this.activatedRoute.queryParams.subscribe(params => {
      this.loadData();
    });
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));

    this.employeesClient.search('', null, null, '').subscribe(x => this.employees = x.data);
    this.valueGroupsClient.getFinancialTransactionTypes().subscribe(res => this.transactionTypes = res);
  }

  loadData() {
    this.searchGridOptions.dataSource.query.fromDate = this.fromDate;
    this.searchGridOptions.dataSource.query.toDate = this.toDate;
    this.searchGridOptions.dataSource.loadData();
  }

  change() {
    alert('ch')
  }

  exportData(withdrawal, deposit) {
    var query = new ExportFinancialTransactionQuery();
    query.fromDate = this.fromDate;
    query.toDate = this.toDate;
    query.withdrawal = withdrawal;
    query.deposit = deposit;
    this.transactionsClient.export(query).subscribe(res => {
      console.log(res);
      var data = res.data.map(x => {
        var obj = {};
        obj["رقم الحركة"] = x.transactionId;
        obj["التاريخ"] = x.date;
        obj["نوع الحركة"] = this.translateService.instant("models.valuegroup.values.financialTransactionType." + x.typeValue);
        obj["المبلغ"] = x.amount;
        obj["بند الحركة"] = x.title;
        obj["اسم الموظف"] = x.employeeName;
        obj["ملاحظات"] = x.remarks;
        return obj;
      });

      const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data)
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      XLSX.writeFile(wb, "الحسابات.xlsx");
    }, error => this.errorHandler(error));
  }

  selectionChanged(e: SelectionChange<any>) {
    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
  }

  onCreateTranasaction() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    config.data = new CreateFinancialTransactionCommand();
    const dialogRef = this.dialog.open(AddFinancialTransactionComponent, config);
    dialogRef.componentInstance.employees = this.employees;
    dialogRef.componentInstance.transactionTypes = this.transactionTypes;
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.transactionsClient.create(result).subscribe(x => {
          this.loadData();
        }, error => this.errorHandler(error));
      }
    });
  }

  onEditTransaction() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    var transactionId = this.searchGridOptions.selectionModel.selected[0].transactionId;
    this.transactionsClient.getById(transactionId).subscribe(res => {
      var updateCommand: UpdateFinancialTransactionCommand = <any>res;
      config.data = updateCommand;
      const dialogRef = this.dialog.open(AddFinancialTransactionComponent, config);
      dialogRef.componentInstance.employees = this.employees;
      dialogRef.componentInstance.transactionTypes = this.transactionTypes;
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.transactionsClient.update(transactionId, result).subscribe(x => {
            this.loadData();
          }, this.errorHandler);
        }
      });
    }, this.errorHandler)
  }

  onDeleteStore() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px"
    var transactionId = this.searchGridOptions.selectionModel.selected[0].transactionId;
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.delete',
      message: this.translateService.instant('messages.financialTransaction.confirmDelete'),
      localize: true
    };
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.transactionsClient.delete(transactionId).subscribe(x => {
          this.loadData();
        }, this.errorHandler);
      }
    });
  }

  onFinancialStatistics() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px";
    var query = new FinancialTransactionsStatisticsQuery();
    query.fromDate = this.fromDate;
    query.toDate = this.toDate;
    this.transactionsClient.statistics(query).subscribe(res => {
      config.data = res;
      const dialogRef = this.dialog.open(FinancialStatisticsComponent, config);
      dialogRef.afterClosed().subscribe(result => {
      });
    });
  }

  errorHandler(error) {
    console.log(error);
  }

}
