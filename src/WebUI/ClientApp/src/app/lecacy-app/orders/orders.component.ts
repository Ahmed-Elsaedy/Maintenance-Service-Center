import { Component, EventEmitter, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { SidePanelService } from '../core/services/side-panel.service';
import { AppActionsService, ActionType, ActionGroup } from '../core/services/app-actions.service';
import { LoaderService } from '../core/services/loader.service';
import { DataApiService } from '../core/services/data-api.service';
import { AlertifyService } from '../core/services/alertify.service';
import { ListComponentBase } from '../core/abstract/list-component.base';
import { isNullOrUndefined } from 'util';
import { FieldType } from '../core/models/query-designer/field-type.enum';
import { ClipboardService } from 'ngx-clipboard';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent extends ListComponentBase {
  private editMode: EditMode = EditMode.Order;
  constructor(private http: HttpClient,
    sidePanel: SidePanelService,
    loaderService: LoaderService,
    private alertifyService: AlertifyService,
    actionsService: AppActionsService,
    private activeRoute: ActivatedRoute,
    dataApi: DataApiService,
    private alertify: AlertifyService,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string,
    private _clipboardService: ClipboardService) {
    super(sidePanel, actionsService, loaderService, dataApi);
    this.title = "Orders";
    this.dataApi.controller = "Order";
    this.dataApi.table = "Orders";
  }

  get actions() {
    return super.actions.filter(x => x.group == ActionGroup.Orders);
  }

  ngOnInit() {
    //this.dtOptions["searching"] = false;
    this.dtOptions["language"] = {
      search: "Search",
      searchPlaceholder: "(Search in report...)"
    };
    this.dtOptions["columns"] = [
      { data: "oid", name: "Oid", title: "OID", type: FieldType.Number },
      { data: "orderid", name: "Orderid", title: "OrderID", sortable: false, type: FieldType.String },
      { data: "dateAssigned", name: "DateAssigned", title: "Date", type: FieldType.Date },
      { data: "customer", name: "Customer", title: "Customer", sortable: false, type: FieldType.String },
      { data: "primaryPhone", name: "PrimaryPhone", title: "Phone", sortable: false, type: FieldType.String },
      { data: "secondaryPhone", name: "SecondaryPhone", title: "Phone2", sortable: false, type: FieldType.String },
      { data: "region", name: "Region", title: "Region", type: FieldType.String },
      { data: "street", name: "Street", title: "Notes", sortable: true, type: FieldType.String },
      { data: "address", name: "Address", title: "Address", sortable: false, type: FieldType.String },
      { data: "product", name: "Product", title: "Product", type: FieldType.String },
      { data: "model", name: "Model", title: "Model", sortable: false, type: FieldType.String },
      { data: "complaint", name: "Complaint", title: "Complaint", sortable: false, type: FieldType.String },
      { data: "ticketCategory", name: "ActiveTicketNavigation.Category", title: "Category", type: FieldType.Lookup },
      { data: "ticketReport", name: "ActiveTicketNavigation.Report", title: "Report", sortable: false, type: FieldType.String },
      { data: "ticketEmployee", name: "ActiveTicketNavigation.Employee", title: "Employee", sortable: true, type: FieldType.Lookup },
    ];
    this.dtOptions["columnDefs"] = [
      {
        // OID + OrderOID
        targets: 0,
        render: function (data, type, row) {
          var d = '(' + data + ') ' + (row.orderid || '');
          if (d.length > 15) {
            d = d.substr(0, 15) + '...';
          }
          return '<span title="' + row.orderid + '">' + d + '</span>'
        }
      },
      {
        // Formatting Date
        targets: 2,
        render: function (data, type, row) {
          return (new Date(data)).toLocaleString();
        }
      },
      {
        // Combining Phones
        targets: 4,
        render: function (data, type, row) {
          return (data || '') + ' ' + (row.secondaryPhone || '');
        }
      },
      {
        // Product + Model
        targets: 9,
        render: function (data, type, row) {
          return data + '<br/>(' + row.model + ')';
        }
      },
      {
        // Region + Address
        targets: 8,
        width: "15%",
        render: function (data, type, row) {
          var d = (row.region || '') + ' ' + (data || '');
          return '<span title="' + d + '">' + (d.length > 40 ? d.substr(0, 40) + "..." : d) + '</span>'
        }
      },
      {
        // Report
        targets: 13,
        width: "15%",
        render: function (data, type, row) {
          if (data)
            return '<span title="' + data + '">' + (data.length > 40 ? data.substr(0, 40) + "..." : data) + '</span>'
          else
            return '';
        }
      },
      {
        targets: 14,
        render: function (data, type, row) {
          return `<span style="border-bottom: 1px dotted;">${(data || '')}</span>` + '<br/>' + (row.ticketCategory || '')
        }
      },
      {
        targets: [1, 5, 10, 12, 6],
        visible: false
      }
    ]
    this.dtOptions["order"] = [[0, "desc"]];
    super.ngOnInit();
    this.loadCategriesAndEmployees();
  }

  private allNotes: any;
  private allRegions: any;
  dtOnAjax(event) {
    this.dataApi.dataTable(event.data).subscribe((res: any) => {
      event.callback(res);
      this.allNotes = res.additionalParameters.regions;
      this.allRegions = res.additionalParameters.notes;
    });
  }

  employees: any[];
  categories: any[];
  loadCategriesAndEmployees() {
    this.dataApi.controller = "Category";
    var query = { select: 'Oid,Title' };
    this.dataApi.query(query).subscribe(res => {
      this.categories = res;
    }, error=>{
      debugger;
    });
    this.dataApi.controller = "Employee";
    query = { select: 'Oid,DisplayName' };
    this.dataApi.query(query).subscribe(res => {
      this.employees = res;
    }, error=>{
      debugger;
    });
    this.dataApi.controller = "Order";
  }

  filterFormGroupValue: any;
  onSidePanelSubmit(data: any) {
    if (this.editMode == EditMode.Order) {
      this.dataApi.controller = "Order";
      super.onSidePanelSubmit(data);
    } else if (this.editMode == EditMode.Report) {
      this.dataApi.controller = "OrderTicket";
      super.onSidePanelSubmit(data);
    } else if (this.editMode == EditMode.Patch) {
      var patchData: any = {
        orders: this.selected.map(x => x.oid),
        orderFields: Object.keys(data.order),
        ticketFields: Object.keys(data.ticket),
        templateOrder: data.order,
        templateTicket: data.ticket
      }
      patchData.tickets = this.selected.filter(x => !isNullOrUndefined(x.activeTicket)).map(x => x.activeTicket);
      this.dataApi.controller = "Order";
      this.dataApi.patch(patchData).subscribe(x => {
        this.sidePanel.closePanel();
        this.dtRedraw.next();
      }, error => {
        debugger
      });
    } else if (this.editMode == EditMode.Query) {
      var result = {};
      Object.keys(data).forEach(key => {
        if (data[key]) {
          result[key] = data[key];
        }
      });
      this.filterContainer = result;
      this.filterFormGroupValue = data;
      this.sidePanel.closePanel();
      this.dtRedraw.next();
      //this.loadingService.show('s');
    }
  }

  onExecuteAction(id) {
    switch (<ActionType>id) {
      case ActionType.NewOrder:
        this.editMode = EditMode.Order;
        var viewModel = { dateAssigned: new Date() }
        var route1 = ['/orders', { outlets: { side: ['new'] } }];
        this.sidePanel.openPanel({ title: 'New Order', data: viewModel }, route1);
        break;

      case ActionType.EditOrder:
        this.editMode = EditMode.Order;
        var row = this.selected[0];
        var route2 = ['/orders', { outlets: { side: ['edit'] } }];
        this.sidePanel.openPanel({ title: `Edit Order (${row.oid})`, data: row }, route2);
        break;

      case ActionType.AddReport:
        this.editMode = EditMode.Report;
        var row = this.selected[0];
        var options: any = {
          categories: this.categories,
          employees: this.employees,
          viewModel: { date: new Date(), order: row.oid }
        }
        route2 = ['/orders', { outlets: { side: ['report'] } }];
        this.sidePanel.openPanel({ title: `New Report`, data: options }, route2);
        break;

      case ActionType.PatchEdit:
        this.editMode = EditMode.Patch;
        options = {
          categories: this.categories,
          employees: this.employees,
          activeTicket: this.selected.filter(x => !x.activeTicket).length == 0
        }
        route2 = ['/orders', { outlets: { side: ['patch'] } }];
        this.sidePanel.openPanel({ title: `Patch Edit`, data: options }, route2);
        break;

      case ActionType.OrderQuery:
        this.editMode = EditMode.Query;
        route2 = ['/orders', { outlets: { side: ['filter'] } }];
        options = {
          "regions": this.allRegions,
          "notes": this.allNotes,
          "categories": this.categories,
          "formGroup": this.filterFormGroupValue
        }
        this.sidePanel.openPanel({ title: `Orders Filter`, data: options }, route2);
        break;

      case ActionType.DeleteOrder:
        var row = this.selected[0];
        this.alertify.confirm("Delete", "Are you sure you want to delete order #" + row.oid,
          () => {
            this.dataApi.delete(row.oid).subscribe(x => {
              this.dtRedraw.next();
            }, error => {
              debugger;
            })
          },
          () => { });
        break;

      case ActionType.CopyOrderToClipboard:
        var orders = this.selected.map(x => {
          var result: string[] = [];
          if (x.oid)
            result.push(x.oid);
          if (x.customer)
            result.push(x.customer);

          var phones = (x.primaryPhone || '') + ' ' + (x.secondaryPhone || '');
          if (phones != ' ')
            result.push(phones);

          if (x.address)
            result.push(x.address);

          var model = (x.product || ' ') + ' ' + (x.model || '');
          if (model != ' ')
            result.push(model);

          if (x.complaint)
            result.push(x.complaint);

          if (x.ticketReport)
            result.push(x.ticketReport);

          result.push(this.baseUrl + 'technician/reports/' + x.oid);

          return result.join('\n');
        });
        var result = orders.join('\n\n');
        this._clipboardService.copyFromContent(result);
        break;

        case ActionType.CopyIDsToClipboard:
          var ordersIds = this.selected.map(x => x.oid);
          var result = ordersIds.join(';');
          this._clipboardService.copyFromContent(result);
          break;
    }
  }

  ngOnDestroy() {
    super.ngOnDestroy();
    this.actionsService.restoreGroup(ActionGroup.Orders);
  }

  dtOnColumnAction(event) {
    console.log(event);
  }
}

enum EditMode {
  Order = 0,
  Report = 1,
  Patch = 2,
  Query = 3
}