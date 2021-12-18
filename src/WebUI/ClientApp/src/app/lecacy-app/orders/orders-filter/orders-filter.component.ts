import { Component } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormBuilder, FormControl } from '@angular/forms';
import { TreeFilter } from 'src/app/lecacy-app/core/models/query-designer/tree-filter.model';
import { TreeFilterType } from 'src/app/lecacy-app/core/models/query-designer/tree-filter-type.enum';

@Component({
  selector: 'app-orders-filter',
  templateUrl: './orders-filter.component.html',
  styleUrls: ['./orders-filter.component.css']
})
export class OrdersFilterComponent extends EditComponentBase {

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel);
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      oid: new FormControl(''),
      customer: new FormControl(''),
      phones: new FormControl(''),
      region: new FormControl(''),
      note: new FormControl(''),
      ticket: new FormControl(''),
    });

    this.selectFieldKeys.push('region');
    this.selectFieldKeys.push('note');
    this.selectFieldKeys.push('ticket');

    super.ngOnInit();
  }

  ngAfterViewInit(): void {
    if (this.select2Sources) {
      this.select2Sources.regions.forEach(reg => {
        this.appendSelect2Option('region', reg, reg, false);
      });
      this.select2Sources.notes.forEach(note => {
        this.appendSelect2Option('note', note, note, false);
      });
      this.select2Sources.categories.forEach(cat => {
        this.appendSelect2Option('ticket', cat.Title || cat.title, cat.Oid || cat.oid, false);
      });
    }
    super.ngAfterViewInit();
  }

  private select2Sources: any;
  onSidePanelOpened(e) {
    if (e.data) {
      this.select2Sources = {
        regions: e.data.notes,
        notes: e.data.regions,
        categories: e.data.categories,
      };
      if (e.data.formGroup)
        this.formGroup.patchValue(e.data.formGroup);
    }
  }

  onValidSubmit() {
    this.sidePanel.submitPanel(this.formGroup.value);
  }

  onClear() {
    this.formGroup.reset();
    $("select#region").val(null);
    $("select#region").trigger('change');
    $("select#note").val(null);
    $("select#note").trigger('change');
    $("select#ticket").val(null);
    $("select#ticket").trigger('change');
    this.sidePanel.submitPanel(this.formGroup.value);
  }
}
