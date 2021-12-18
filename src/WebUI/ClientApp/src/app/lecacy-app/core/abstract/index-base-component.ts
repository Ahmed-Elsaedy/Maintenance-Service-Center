import { ToolbarService } from '../services/toolbar.service';
import { SidePanelService } from '../services/side-panel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ViewChild } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { ToolbarAction } from '../models/toolbar-action';

export class IndexComponentBase<T> {
    protected title: string;
    protected selectedItems: T[] = [];

    @ViewChild(DataTableDirective, { static: false })
    protected datatableElement: DataTableDirective;
    protected dtOptions: any = {};

    constructor(private toolbarService: ToolbarService,
        protected sidePanelService: SidePanelService,
        private activeRoute: ActivatedRoute,
        protected router: Router,
        protected location: Location) {
    }

    ngOnInit() {
        this.toolbarService.title = this.title;
        this.toolbarService.actions = [];
        this.dtOptions = this.onGetDataTableOptions();
        this.onConfigureToolbarAction();
    }

    onConfigureToolbarAction() { }

    ngAfterViewInit(): void {
        this.datatableElement.dtInstance.then(api => {
            api.on('select', (e, dt, type, indexes) => {
                this.updateSelectedItems(api.rows({ selected: true }).data());
            })
                .on('deselect', (e, dt, type, indexes) => {
                    this.updateSelectedItems(api.rows({ selected: true }).data());
                })
                .on('page', (e) => {
                    (api.rows() as any).deselect();
                    this.updateSelectedItems(null);
                });
        });
    }

    private updateSelectedItems(data) {
        this.selectedItems = [];
        if (data)
            data.each(obj => this.selectedItems.push(obj as T));
        this.updateToolbarActions();
    }

    redrawDataTableRow(oldData, newData) {
        this.datatableElement.dtInstance.then(api => {
            //api.ajax.reload();
            (api.rows() as any).deselect();
            //this.location.back();
            api.rows(oldData).data(newData).draw(false);
            this.updateToolbarActions();
        });
    }

    redrawDataTableView() {
        this.datatableElement.dtInstance.then(api => {
            (api.rows() as any).deselect();
            api.ajax.reload(null, false);
            this.updateToolbarActions();
        });
    }

    attachToolbarAction(id: number, title: string, enabled?: boolean) {
        var action = new ToolbarAction(id, title, enabled);
        action.execute = () => this.onActionExecute(id);
        this.toolbarService.actions.push(action);
    }

    private updateToolbarActions() {
        this.toolbarService.actions.forEach((action: ToolbarAction) => {
            action.disabled = !this.onActionCanExecute(action.id);
        });
    }

    onGetDataTableOptions() { }
    onActionExecute(action: number) { }
    onActionCanExecute(action: number) { return false; }
}
