import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ListComponentBase } from 'src/app/lecacy-app/core/abstract/list-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { AppActionsService, ActionGroup, ActionType } from 'src/app/lecacy-app/core/services/app-actions.service';
import { LoaderService } from 'src/app/lecacy-app/core/services/loader.service';
import { DataApiService } from 'src/app/lecacy-app/core/services/data-api.service';
import { AlertifyService } from 'src/app/lecacy-app/core/services/alertify.service';
import { HttpClient } from '@angular/common/http';
import { ElarabyApiService } from 'src/app/lecacy-app/core/services/elaraby-api.service';
import { IndexWorkOrder } from 'src/app/lecacy-app/core/models/elaraby/IndexWorkOrder';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-work-orders',
  templateUrl: './work-orders.component.html',
  styleUrls: ['./work-orders.component.css']
})
export class WorkOrdersComponent extends ListComponentBase {
  constructor(sidePanel: SidePanelService,
    actionsService: AppActionsService,
    loadingService: LoaderService,
    private alertify: AlertifyService,
    private elarabyApi: ElarabyApiService) {
    super(sidePanel, actionsService, loadingService, null);
    this.title = "Work Orders";
  }

  get actions() {
    return super.actions.filter(x => x.group == ActionGroup.Elaraby);
  }

  ngOnInit() {
    this.dtOptions = {
      "paging": false,
      "ordering": false,
      "info": false,
      "select": true,
      "ajax": (data, callback, settings) => {
        this.dtOnAjax({ data, callback, settings });
      },
      "columns": [
        { data: "OrderNumber", name: "", title: "Number" },
        { data: "CustomerName", name: "", title: "Customer" },
        { data: "AreaName", name: "", title: "Area" },
        { data: "LandLine", name: "", title: "Phones" },
        { data: "Address", name: "", title: "Address" },
        { data: "ProductType", name: "", title: "Product" },
        { data: "ProblemCliet", name: "", title: "Problem" },
        { data: "MobileNumber", name: "", title: "Mobile" },
        { data: "ScheduledStartDate", name: "", title: "Date" },
        { data: "BackOfficeNotes", name: "", title: "Notes" },
        { data: "WorkOrderType", name: "", title: "Type" },
        { data: "HasComplaint", name: "", title: "Complaint" },
        { data: "SapNumber", name: "", title: "SAP" }
      ],
      "columnDefs": [
        {
          targets: 0,
          render: function (data, type, row) {
            return (row.Oid ? (row.Oid + '<br/>') : '') + data + '<br/>' + (row.SapNumber || '');
          }
        }, {
          targets: 3,
          render: function (data, type, row) {
            return data + '<br/>' + (row.MobileNumber || '');
          }
        }, {
          targets: 9,
          render: function (data, type, row) {
            var result = data;
            if (result.length > 30) {
              result = data.substr(0, 30) + '...';
              return '<span title="' + data + '">' + result + '</span>'
            } else return data;
          }
        }, {
          targets: [7, 2, 12],
          visible: false
        }
      ]
    };
    super.ngOnInit();
  }

  dtOnAjax(event) {
    this.elarabyApi.getWorkOrders().subscribe(res => {
      event.callback(res);
    });
  }

  dtOnSelectionChanged(data: any[]) {
    super.dtOnSelectionChanged(data);
    if (data.length == 1 && !data[0].Oid)
      this.actions[0].disabled = false;
    else
      this.actions[0].disabled = true;
  }

  onSidePanelSubmit(data) {
    data.oid = 0;
    data.activeTicket = null;
    this.elarabyApi.download(data).subscribe(x => {
      this.sidePanel.closePanel();
      var rowData = this.selected[0];
      rowData.Oid = x;
      this.dtRedrawRow.next(rowData);
      this.dtSelectNone.next();
    });
  }

  onExecuteAction(id) {
    switch (<ActionType>id) {
      case ActionType.DownloadWorkOrders:

        var obj = {
          oid: '',
          orderid: this.selected[0].OrderNumber,
          dateAssigned: new Date(),
          customer: this.selected[0].CustomerName,
          primaryPhone: this.selected[0].LandLine,
          secondaryPhone: this.selected[0].MobileNumber,
          region: this.selected[0].AreaName,
          product: this.selected[0].ProductType,
          model: this.selected[0].ProductType,
          street: '',
          address: this.selected[0].Address,
          complaint: this.selected[0].ProblemCliet + " " + this.selected[0].BackOfficeNotes,
          activeTicket: '',
          workOrderId: this.selected[0].WorkOrderId,
          sapNumber: this.selected[0].SapNumber
        }

        var route1 = ['/elaraby', { outlets: { side: ['download'] } }];
        this.sidePanel.openPanel({ title: 'New Order', data: obj }, route1);

        break;
    }
  }
}
