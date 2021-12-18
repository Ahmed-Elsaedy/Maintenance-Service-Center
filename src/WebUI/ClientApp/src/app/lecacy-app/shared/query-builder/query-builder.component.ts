import { Component, OnInit } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { FieldType } from 'src/app/lecacy-app/core/models/query-designer/field-type.enum';
import { WhereFilterType } from 'src/app/lecacy-app/core/models/query-designer/where-filter-type.enum';

@Component({
  selector: 'app-query-builder',
  templateUrl: './query-builder.component.html',
  styleUrls: ['./query-builder.component.css']
})
export class QueryBuilderComponent extends EditComponentBase {

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel);
  }
  ngOnInit() {
    this.formGroup = this.fb.group({
      field: new FormControl('', [Validators.required]),
      operator: new FormControl('', [Validators.required]),
      value: new FormControl('')
    });

    this.selectFieldKeys.push('field');
    this.selectFieldKeys.push('operator');
    super.ngOnInit();
  }

  ngAfterViewInit(): void {
    if (this.select2Sources) {
      this.select2Sources.columns.forEach(col => {
        this.appendSelect2Option('field', col.title, col.name, false);
      });
    }
    super.ngAfterViewInit();
  }

  private operators: any[];
  onSelect2Change(e) {
    super.onSelect2Change(e);
    if ($(e.target).attr('id') == 'field') {
      var col = this.select2Sources.columns.filter(x => x.name == e.target.value)[0];
      var operators = this.getWhereFilterTypeByFieldType(col.type);
      super.emptySelect2('operator');
      operators.forEach(x => {
        this.appendSelect2Option('operator', WhereFilterType[x.whereType], x.whereType, false);
      });
    }
  }

  private select2Sources: any;
  onSidePanelOpened(e) {
    if (e.data) {
      this.select2Sources = {
        columns: e.data.columns,
        categories: e.data.categories,
        employees: e.data.employees
      };


    }
  }

  getWhereFilterTypeByFieldType(type: FieldType) {
    switch (type) {
      case FieldType.Number:
      case FieldType.Date:
        return [
          { whereType: WhereFilterType.Equal, requiresValue: true },
          { whereType: WhereFilterType.GreaterThan, requiresValue: true },
          { whereType: WhereFilterType.GreaterThanOrEqual, requiresValue: true },
          { whereType: WhereFilterType.LessThan, requiresValue: true },
          { whereType: WhereFilterType.LessThanOrEqual, requiresValue: true }
        ];
      case FieldType.String:
        return [
          { whereType: WhereFilterType.Contains, requiresValue: true },
          { whereType: WhereFilterType.NotContains, requiresValue: true },
          { whereType: WhereFilterType.IsNullOrEmpty, requiresValue: false },
          { whereType: WhereFilterType.IsNotNullOrEmpty, requiresValue: false },
        ];
    }
  }
}
