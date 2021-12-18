import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ListComponentBase } from '../core/abstract/list-component.base';
import { SidePanelService } from '../core/services/side-panel.service';
import { AppActionsService, ActionGroup, ActionType } from '../core/services/app-actions.service';
import { LoaderService } from '../core/services/loader.service';
import { DataApiService } from '../core/services/data-api.service';
import { AlertifyService } from '../core/services/alertify.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent extends ListComponentBase {

  constructor(sidePanel: SidePanelService,
    actionsService: AppActionsService,
    loadingService: LoaderService,
    dataApi: DataApiService,
    private alertify: AlertifyService,
    private http: HttpClient) {
    super(sidePanel, actionsService, loadingService, dataApi);
    this.title = "Employees";
    this.dataApi.controller = "Employee";
    this.dataApi.table = "Employees";
  }

  get actions() {
    return super.actions.filter(x => x.group == ActionGroup.Employees);
  }

  ngOnInit() {
    this.dtOptions["columns"] = [
      { data: "oid", name: "Oid", title: "OID" },
      { data: "displayName", name: "DisplayName", title: "Name" },
      { data: "primaryPhone", name: "PrimaryPhone", title: "Phone 1" },
      { data: "secondaryPhone", name: "SecondaryPhone", title: "Phone 2" },
      { data: "isActive", name: "IsActive", title: "Active" },
    ];
    this.dtOptions["order"] = [[0, "desc"]];

    super.ngOnInit();
  }

  onExecuteAction(id) {
    switch (<ActionType>id) {
      case ActionType.NewEmployee:
        var route1 = ['/employees', { outlets: { side: ['new'] } }];
        this.sidePanel.openPanel({ title: 'New Employee', data: null }, route1);
        break;

      case ActionType.EditEmployee:
        var row = this.selected[0];
        let id: number = row.oid;
        var route2 = ['/employees', { outlets: { side: [id, 'edit'] } }];
        this.sidePanel.openPanel({ title: `Edit Employee (${id})`, data: row }, route2);
        break;

        case ActionType.DeleteEmployee:
          var row = this.selected[0];
          this.alertify.confirm("Delete", "Are you sure you want to delete employee #" + row.oid,
            () => {
              this.dataApi.delete(row.oid).subscribe(x => {
                this.dtRedraw.next();
              }, error => {
                debugger;
              })
            },
            () => { });
          break;
    }
  }

  ngOnDestroy() {
    super.ngOnDestroy();
    this.actionsService.restoreGroup(ActionGroup.Employees);
  }
}
