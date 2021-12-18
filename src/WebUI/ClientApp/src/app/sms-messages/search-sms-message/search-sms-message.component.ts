import { Component, OnInit } from '@angular/core';
import { Direction } from '@angular/cdk/bidi';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateSMSMessageCommand, EmployeeDto, EmployeesClient, UpdateSMSMessageCommand, IUpdateSMSMessageCommand, SMSMessagesClient, SMSMessageDto } from 'src/app/ElarabyCA-api';
import { IDataGridOptions } from 'src/app/shared/data-grid/data-grid.component';
import { TranslateService } from '@ngx-translate/core';
import { IDataToolbarOptions } from 'src/app/shared/data-toolbar/data-toolbar.component';
import { SelectionModel, SelectionChange } from '@angular/cdk/collections';
import { MessageBoxComponent } from 'src/app/shared/message-box/message-box.component';
import { Router, NavigationExtras } from '@angular/router';
import { SMSMessagesDataSource } from '../sms-messages.datasource';
import { SMSMessageValidations } from '../sms-message.validations';
import { EditSMSMessageComponent } from '../edit-sms-message/edit-sms-message.component';
import { DatePipe } from '@angular/common';

export enum SMSMessageAction {
  Create = 0,
  Edit = 1,
  Delete = 2,
  Details = 3
}

export function getDirection() {
  return <Direction>document.querySelector('html').getAttribute('dir');
}

const StringFormat = (str: string, ...args: string[]) =>
  str.replace(/{(\d+)}/g, (match, index) => args[index] || '')

@Component({
  selector: 'app-search-sms-message',
  templateUrl: './search-sms-message.component.html',
  styleUrls: ['./search-sms-message.component.css']
})
export class SearchSMSMessageComponent implements OnInit {
  toolbarOptions: IDataToolbarOptions;
  searchGridOptions: IDataGridOptions;

  constructor(public dialog: MatDialog,
    private smsMessagesClient: SMSMessagesClient,
    private smsMessageValidations: SMSMessageValidations,
    private datePipe: DatePipe,
    private router: Router,
    private translateService: TranslateService) { }

  ngOnInit() {
    this.toolbarOptions = {
      icon: 'message',
      title: 'topMenu.smsmessages',
      actions: [
        { id: SMSMessageAction.Create, title: "indexView.create", icon: "add_circle_outline", execute: () => this.onCreateSMSMessage() },
        { id: SMSMessageAction.Edit, title: "indexView.edit", icon: "edit", disabled: true, execute: () => this.onEditSMSMessage() },
        { id: SMSMessageAction.Delete, title: "indexView.delete", icon: "delete", disabled: true, execute: () => this.onDeleteSMSMessage() },
        { id: SMSMessageAction.Details, title: "indexView.smsMessageDetails", icon: "save_alt", disabled: true, execute: () => this.onSMSMessageDetails() }
      ],
      localize: true
    }

    this.searchGridOptions = {
      columns: [
        { title: 'models.smsmessage.id', name: 'id', width: '100px', sortable: true },
        {
          title: 'models.smsmessage.created', name: 'created', width: '120px', sortable: true,
          mapper: (obj) => this.datePipe.transform(obj.created, 'medium')
        },
        { title: 'models.smsmessage.title', name: 'title', sortable: true },
        { title: 'models.smsmessage.text', name: 'text' },
        { title: 'models.smsmessage.ordersCount', name: 'ordersCount' }
      ],
      dataSource: new SMSMessagesDataSource(this.smsMessagesClient),
      selectionModel: new SelectionModel<any>(false, []),
      localize: true
    };
    this.searchGridOptions.dataSource.loadData();
    this.searchGridOptions.selectionModel.changed.subscribe(x => this.selectionChanged(x));
  }

  selectionChanged(e: SelectionChange<any>) {
    this.toolbarOptions.actions[1].disabled = this.searchGridOptions.selectionModel.selected.length != 1;
    this.toolbarOptions.actions[2].disabled = this.searchGridOptions.selectionModel.selected.length == 0;
    this.toolbarOptions.actions[3].disabled = this.searchGridOptions.selectionModel.selected.length == 0;
  }

  onCreateSMSMessage() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    config.data = new CreateSMSMessageCommand();
    const dialogRef = this.dialog.open(EditSMSMessageComponent, config);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.smsMessagesClient.create(result).subscribe(x => {
          this.searchGridOptions.selectionModel.clear();
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  onEditSMSMessage() {
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "600px"
    var smsMessageId = this.searchGridOptions.selectionModel.selected[0].id;
    this.smsMessagesClient.getById(smsMessageId).subscribe(res => {
      var updateCommand: UpdateSMSMessageCommand = <any>res;
      config.data = updateCommand;
      const dialogRef = this.dialog.open(EditSMSMessageComponent, config);
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.smsMessagesClient.update(smsMessageId, result).subscribe(x => {
            this.searchGridOptions.selectionModel.clear();
            this.searchGridOptions.dataSource.loadData();
          }, (e) => this.errorHandler(e));
        }
      });
    }, (e) => this.errorHandler(e))
  }

  onDeleteSMSMessage() {
    var smsMessage = this.searchGridOptions.selectionModel.selected[0];
    // var error = this.smsMessageValidations.validateDeleteSMSMessage(smsMessage);
    // if (error) {
    //   this.validationErrorHandler(error);
    //   return;
    // }
    const config = new MatDialogConfig();
    config.direction = getDirection();
    config.width = "400px"
    var smsMessageId = smsMessage.id;
    const dialogRef = this.dialog.open(MessageBoxComponent, config);
    dialogRef.componentInstance.options = {
      title: 'dialogTitles.delete',
      message: this.translateService.instant('messages.smsmessage.confirmDelete'),
      localize: true
    };
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.smsMessagesClient.delete(smsMessageId).subscribe(x => {
          this.searchGridOptions.selectionModel.clear();
          this.searchGridOptions.dataSource.loadData();
        }, (e) => this.errorHandler(e));
      }
    });
  }

  onSMSMessageDetails() {
    var smsMessage: SMSMessageDto = this.searchGridOptions.selectionModel.selected[0];
    this.router.navigate(['/', 'sms', 'orders'],
      { queryParams: { "smsmessage_id": smsMessage.id, "title": smsMessage.title } });
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
