import { OnDestroy, OnInit } from '@angular/core';
import { Subscription, Subject } from 'rxjs';
import { SidePanelService } from '../services/side-panel.service';
import { AppActionsService, SelectionMode } from '../services/app-actions.service';
import { LoaderService } from '../services/loader.service';
import { DataApiService } from '../services/data-api.service';
import { AppAction } from '../enums/app-actions.enum';

export class ListComponentBase implements OnInit, OnDestroy {
    protected subs: Subscription[] = [];
    dtOptions: any;
    selected: any[];
    title: string;
    dtRedraw: Subject<any> = new Subject();
    dtRedrawRow: Subject<any> = new Subject();
    dtSelectNone: Subject<any> = new Subject();
    filterContainer: any;

    constructor(protected sidePanel: SidePanelService,
        protected actionsService: AppActionsService,
        protected loadingService: LoaderService,
        protected dataApi: DataApiService) {
        this.dtOptions = {
            "processing": true,
            "serverSide": true,
            "select": true,
            "pageLength": 10,
            "ajax": (data, callback, settings) => {
                data["additionalParameters"] = this.filterContainer;
                this.dtOnAjax({ data, callback, settings });
            },
        };
    }

    get actions() {
        return this.actionsService.actions.filter(x=> x.visible);
    }



    ngOnInit() {
        this.subs.push(this.sidePanel.panelSubmitted.subscribe(
            data => this.onSidePanelSubmit(data)));
    }

    ngOnDestroy(): void {
        this.subs.forEach(sub => {
            sub.unsubscribe();
        });
    }

    dtOnAjax(event) {
        this.dataApi.dataTable(event.data).subscribe(res => {
            event.callback(res);
        });
    }

    onSidePanelSubmit(data) {
        if (!data.oid) {
            data.oid = 0;
            this.dataApi.create(data).subscribe(x => {
                this.sidePanel.closePanel();
                this.dtRedraw.next();
            });
        }
        else {
            this.dataApi.update(data.oid, data).subscribe(x => {
                this.sidePanel.closePanel();
                this.dtRedraw.next();
            });
        }
    }

    dtOnSelectionChanged(data: any[]) {
        this.selected = data;

        this.actions.filter(x => x.selMode == SelectionMode.Single)
            .forEach(a => a.disabled = data.length != 1);

        this.actions.filter(x => x.selMode == SelectionMode.Multi)
            .forEach(a => a.disabled = data.length == 0);
    }

    onExecuteAction(id) {
    }
}
