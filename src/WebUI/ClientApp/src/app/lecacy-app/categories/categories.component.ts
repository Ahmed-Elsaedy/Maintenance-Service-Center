import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ListComponentBase } from '../core/abstract/list-component.base';
import { SidePanelService } from '../core/services/side-panel.service';
import { AppActionsService, ActionGroup, ActionType } from '../core/services/app-actions.service';
import { LoaderService } from '../core/services/loader.service';
import { DataApiService } from '../core/services/data-api.service';
import { AlertifyService } from '../core/services/alertify.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html'
})
export class CategoriesComponent extends ListComponentBase {

  constructor(sidePanel: SidePanelService,
    actionsService: AppActionsService,
    loadingService: LoaderService,
    dataApi: DataApiService,
    private alertify: AlertifyService,
    private http: HttpClient) {
    super(sidePanel, actionsService, loadingService, dataApi);
    this.title = "Categories";
    this.dataApi.controller = "Category";
    this.dataApi.table = "Categories";
  }

  get actions() {
    return super.actions.filter(x => x.group == ActionGroup.Categories);
  }

  ngOnInit() {
    this.dtOptions["columns"] = [
      { data: "oid", name: "Oid", title: "OID" },
      { data: "title", name: "Title", title: "Title" }
    ];
    this.dtOptions["columnDefs"] = [
      {
        targets: 0,
        width: "10%",
      }
    ];
    this.dtOptions["order"] = [[0, "desc"]];

    super.ngOnInit();
    
  }

  dtOnAjax(event) {
    //event.callback({"draw":1,"recordsTotal":11,"recordsFiltered":11,"data":[{"oid":11,"title":"حسابات مالية"},{"oid":10,"title":"ملاحظات ادارية"},{"oid":9,"title":"متوقف على سحب لمركز الخدمة"},{"oid":8,"title":"شكوى مفتوحة الزيارة"},{"oid":7,"title":"شكوى متوقفه على قطع غيار"},{"oid":6,"title":"الجهاز متواجد بمركز الخدمة"},{"oid":5,"title":"بلاغ مفتوح الزيارة"},{"oid":4,"title":"بلاغ معلق قيد المراجعة"},{"oid":3,"title":"بلاغ متوقف على قطع غيار"},{"oid":2,"title":"متابعة امر شغل خارج الضمان"}],"error":null,"additionalParameters":null});
    this.dataApi.dataTable(event.data).subscribe(res => {
        event.callback(res);
    });
}

  onExecuteAction(id) {
    switch (<ActionType>id) {
      case ActionType.NewCategory:
        var route1 = ['/categories', { outlets: { side: ['new'] } }];
        this.sidePanel.openPanel({ title: 'New Category', data: null }, route1);
        break;

      case ActionType.EditCategory:
        var row = this.selected[0];
        let id: number = row.oid;
        var route2 = ['/categories', { outlets: { side: [id, 'edit'] } }];
        this.sidePanel.openPanel({ title: `Edit Category (${id})`, data: row }, route2);
        break;

      case ActionType.DeleteCategory:
        var row = this.selected[0];
        this.alertify.confirm("Delete", "Are you sure you want to delete category #" + row.oid,
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
    this.actionsService.restoreGroup(ActionGroup.Categories);
  }
}
