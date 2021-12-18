import { Component, OnInit } from '@angular/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { SMSMessagesClient, OrderSMSMessagesClient, AddOrdersForSMSMessageCommand, SendSMSMessageCommand } from 'src/app/ElarabyCA-api';
import { SMSMessageValidations } from '../sms-message.validations';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { getDirection } from 'src/app/stores/search-store/search-store.component';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';
import { SMSMessageOrdersDataSource } from '../sms-message-orders.datasource';
import { EditOrderPhonesComponent, isValidPhoneNumber } from '../edit-order-phones/edit-order-phones.component';
import { AddSmsMessageOrdersComponent } from '../add-sms-message-orders/add-sms-message-orders.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-sms-message-orders',
  templateUrl: './sms-message-orders.component.html',
  styleUrls: ['./sms-message-orders.component.css']
})
export class SmsMessageOrdersComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;
  smsmessage_id: number;

  constructor(public dialog: MatDialog,
    private smsMessagesClient: SMSMessagesClient,
    private smsMessageValidations: SMSMessageValidations,
    private orderSMSMessagesClient: OrderSMSMessagesClient,
    private router: Router,
    private datePipe: DatePipe,
    private activatedRoute: ActivatedRoute,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'message',
      title: 'topMenu.smsmessages',
      actions: [
        { id: 1, title: "indexView.send", icon: "send", disabled: true, execute: () => this.onٍSendOrderSMSMessage() },
        { id: 2, title: "indexView.delete", icon: "delete", disabled: true, execute: () => this.onٍDeleteOrderSMSMessage() },
        { id: 3, title: "indexView.changeOrderPhones", icon: "edit", disabled: true, execute: () => this.onChangeOrderPhones() },
        { id: 4, title: "indexView.addOrdersForSMS", icon: "add_circle_outline", execute: () => this.onAddOrdersForSMS() },
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        //{ title: 'models.orderSMSMessage.id', name: 'id', width: '100px', sortable: true },
        { title: 'models.orderSMSMessage.orderId', name: 'orderId', sortable: true },
        { title: 'models.orderSMSMessage.customer', name: 'customer', sortable: true },
        { title: 'models.orderSMSMessage.primaryPhone', name: 'primaryPhone' },
        { title: 'models.orderSMSMessage.secondaryPhone', name: 'secondaryPhone' },
        {
          title: 'models.orderSMSMessage.report', name: 'report', mapper: (o) => {
            let r: string = o.report;
            if (r.toLowerCase().indexOf('sent') != -1)
              return this.translateService.instant("models.orderSMSMessage.values.report.sent");
            else if (r.toLowerCase().indexOf('invalid') != -1)
              return this.translateService.instant("models.orderSMSMessage.values.report.invalid");
            else if (r.toLowerCase().indexOf('balance') != -1)
              return this.translateService.instant("models.orderSMSMessage.values.report.balance");
            else if (r.toLowerCase().indexOf('error') != -1)
              return this.translateService.instant("models.orderSMSMessage.values.report.error");
            else
              return o.report;
          }
        },
        {
          title: 'models.orderSMSMessage.lastSent', name: 'lastSent', mapper: (o) => {
            return this.datePipe.transform(o.lastSent, 'medium');
          }
        },
        { title: 'models.orderSMSMessage.phone', name: 'phone' },
        { title: 'models.orderSMSMessage.sendCount', name: 'sendCount' }
      ],
      dataSource: new SMSMessageOrdersDataSource(this.orderSMSMessagesClient),
      selectionModel: new SelectionModel<any>(true, []),
      localize: true
    };

    this.activatedRoute.queryParams.subscribe(params => {
      this.smsmessage_id = +params['smsmessage_id'];
      this.toolbarOptions.subTitle = params["title"] ? ` - ${params["title"]}` : '';
      this.searchGridOptions.dataSource.query.sMSMessageId = this.smsmessage_id;
      this.searchGridOptions.selectionModel.clear();
      this.searchGridOptions.dataSource.loadData();
    });

    this.searchGridOptions.selectionModel.clear();
    this.searchGridOptions.dataSource.loadData();
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));
  }

  selectionChanged(e: SelectionChange<any>) {

    this.toolbarOptions.actions[0].disabled = this.searchGridOptions.selectionModel.selected.length == 0;

    let sum: number = 0;
    this.searchGridOptions.selectionModel.selected.forEach(a => sum += parseInt(a.sendCount));

    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length == 0 || sum != 0;
    this.searchGridOptions.selectionModel.selected.length == 0;


    this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
  }

  onٍSendOrderSMSMessage() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "500px"
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'Demo Version',
      message: "This is a demo version, SMS is not supported, Do you want to close?",
      localize: true,
      isNotification: false
    };

    return;
    var selection = [...this.searchGridOptions.selectionModel.selected];
    var invalid = [];
    var commands: SendSMSMessageCommand[] = [];
    selection.forEach(sms => {
      var command = new SendSMSMessageCommand();
      command.orderSMSMessageId = sms.id;
      if (sms.primaryPhone && isValidPhoneNumber(sms.primaryPhone)) {
        command.useSecondary = false;
        commands.push(command);
      } else if (sms.secondaryPhone && isValidPhoneNumber(sms.secondaryPhone)) {
        command.useSecondary = true;
        commands.push(command);
      } else {
        invalid.push(sms);
      }
    });
    if (invalid.length > 0) {
      this.searchGridOptions.selectionModel.clear();
      invalid.forEach(invalid => {
        this.searchGridOptions.selectionModel.toggle(invalid);
      });
      const config = new MatDialogConfig();
      config.direction = getDirection();
      config.width = "500px"
      const dialogRef = this.dialog.open(MessageBoxComponent, config);
      dialogRef.componentInstance.options = {
        title: 'dialogTitles.error',
        message: this.translateService.instant('messages.smsmessage.reviewWrongPhoneNumbers'),
        localize: true,
        isNotification: true
      };
    } else if (commands.length > 0) {
      const config = new MatDialogConfig();
      config.direction = getDirection();
      config.width = "500px"
      const dialogRef = this.dialog.open(MessageBoxComponent, config);
      dialogRef.componentInstance.options = {
        title: 'dialogTitles.sendSMSMessageOrdersConfirm',
        message: this.translateService.instant('messages.orderSMSMessage.sendSMSMessageOrdersConfirm'),
        localize: true,
        isNotification: false
      };
      dialogRef.afterClosed().subscribe(res => {
        if (res) {
          this.searchGridOptions.selectionModel.clear();
          this.runCommand(0, commands);
        }
      })
    }
  }

  runCommand(index: number, collection: SendSMSMessageCommand[]) {
    var c = collection[index];
    var row = this.searchGridOptions.dataSource.data.filter(x => x.id == c.orderSMSMessageId)[0];
    row.report = "...";
    this.smsMessagesClient.send(c).subscribe(res => {
      row = this.searchGridOptions.dataSource.data.filter(x => x.id == c.orderSMSMessageId)[0];
      row.report = res.report;
      row.lastSent = res.lastSent;
      row.phone = res.phone;
      row.sendCount = res.sendCount;
    }, error => {
      row = this.searchGridOptions.dataSource.data.filter(x => x.id == c.orderSMSMessageId)[0];
      row.report = 'Error Occurred';
      this.errorHandler(error);
    }, () => {
      if ((index + 1) < collection.length) {
        this.runCommand((index + 1), collection);
      }
    });
  }

  onٍDeleteOrderSMSMessage() {
    var selection = this.searchGridOptions.selectionModel.selected.map(x => x.id);
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px"
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.delete',
      message: this.translateService.instant('messages.orderSMSMessage.confirmDelete'),
      localize: true
    };
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.orderSMSMessagesClient.delete(selection).subscribe(x => {
          this.searchGridOptions.selectionModel.clear();
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  onChangeOrderPhones() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    var selected = this.searchGridOptions.selectionModel.selected[0];
    config.data = selected;
    const dialogRef = this.dialog.open(EditOrderPhonesComponent, config);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.orderSMSMessagesClient.update(selected.orderId, result).subscribe(x => {
          this.searchGridOptions.selectionModel.clear();
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  onAddOrdersForSMS() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    const dialogRef = this.dialog.open(AddSmsMessageOrdersComponent, config);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        var data = new AddOrdersForSMSMessageCommand();
        data.smsMessageId = this.smsmessage_id;
        data.ordersIds = result;
        this.orderSMSMessagesClient.addOrders(data).subscribe(x => {
          this.searchGridOptions.selectionModel.clear();
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
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
