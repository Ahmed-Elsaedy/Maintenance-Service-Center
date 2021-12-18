import { Component, OnInit } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ListComponentBase } from '../core/abstract/list-component.base';
import { SidePanelService } from '../core/services/side-panel.service';
import { AppActionsService, ActionGroup, ActionType } from '../core/services/app-actions.service';
import { LoaderService } from '../core/services/loader.service';
import { DataApiService } from '../core/services/data-api.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html'
})
export class TicketsComponent extends ListComponentBase {

  constructor(sidePanel: SidePanelService,
    actionsService: AppActionsService,
    loadingService: LoaderService,
    dataApi: DataApiService) {
    super(sidePanel, actionsService, loadingService, dataApi);
    this.title = "Orders Tickets";
    this.dataApi.controller = "OrderTicket";
    this.dataApi.table = "Tickets";
  }

  get actions() {
    return super.actions.filter(x => x.group == ActionGroup.Tickets);
  }

  ngOnInit() {
    this.dtOptions["columns"] = [
      { data: "oid", name: "Oid", title: "OID" },
      { data: "order", name: "Order", title: "Order" },
      { data: "date", name: "Date", title: "Date" },
      { data: "categoryTitle", name: "CategoryNavigation.Title", title: "Category" },
      { data: "report", name: "Report", title: "Report" },
      { data: "employeeName", name: "EmployeeName", title: "Technician" },
      { data: "userName", name: "UserName", title: "Employee" }
    ];
    this.dtOptions["columnDefs"] = [
      {
        targets: 2,
        render: function (data, type, row) {
          return (new Date(data)).toLocaleString();
        }
      }
    ];
    this.dtOptions["order"] = [[0, "desc"]];
    super.ngOnInit();
    this.loadCategriesAndEmployees();
  }

  employees: any[];
  categories: any[];
  loadCategriesAndEmployees() {
    this.dataApi.controller = "Category";
    var query = { select: 'Oid,Title' };
    this.dataApi.query(query).subscribe(res => {
      this.categories = res;
    });
    this.dataApi.controller = "Employee";
    query = { select: 'Oid,DisplayName' };
    this.dataApi.query(query).subscribe(res => {
      this.employees = res;
    });
    this.dataApi.controller = "OrderTicket";
  }

  onExecuteAction(id) {
    switch (<ActionType>id) {
      case ActionType.NewTicket:
        var route1 = ['/tickets', { outlets: { side: ['new'] } }];
        var options = {
          categories: this.categories,
          employees: this.employees,
          viewModel: { date: new Date() }
        }
        this.sidePanel.openPanel({ title: 'New Ticket', data: options }, route1);
        break;

      case ActionType.EditTicket:
        var row = this.selected[0];
        let id: number = row.oid;
        var route2 = ['/tickets', { outlets: { side: [id, 'edit'] } }];
        options = {
          categories: this.categories,
          employees: this.employees,
          viewModel: row
        }
        this.sidePanel.openPanel({ title: `Edit Ticket (${id})`, data: options }, route2);
        break;
    }
  }

  ngOnDestroy() {
    super.ngOnDestroy();
    this.actionsService.restoreGroup(ActionGroup.Tickets);
  }
}