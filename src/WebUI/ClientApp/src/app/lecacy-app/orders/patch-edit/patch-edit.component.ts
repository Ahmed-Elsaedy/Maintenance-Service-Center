import { Component } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
declare var $: any;

@Component({
  selector: 'app-patch-edit',
  templateUrl: './patch-edit.component.html',
  styleUrls: ['./patch-edit.component.css']
})
export class PatchEditComponent extends EditComponentBase {

  private fieldsSource: { name: string, title: string, forTicket: boolean }[] = [
    { name: 'orderid', title: 'Order OID', forTicket: false },
    { name: 'customer', title: 'Customer', forTicket: false },
    { name: 'primaryPhone', title: 'Phone 1', forTicket: false },
    { name: 'secondaryPhone', title: 'Phone 2', forTicket: false },
    { name: 'region', title: 'Region', forTicket: false },
    { name: 'product', title: 'Product', forTicket: false },
    { name: 'model', title: 'Model', forTicket: false },
    { name: 'street', title: 'Notes', forTicket: false },
    { name: 'address', title: 'Address', forTicket: false },
    { name: 'complaint', title: 'Complaint', forTicket: false },
    { name: 'report', title: 'Report', forTicket: true },
    { name: 'employee', title: 'Employee', forTicket: true },
    { name: 'category', title: 'Category', forTicket: true },
  ]

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel);
  }

  ngOnInit() {
    this.formGroup = this.fb.group({

      orderid: new FormControl({ value: '', disabled: true }),
      customer: new FormControl({ value: '', disabled: true }),
      primaryPhone: new FormControl({ value: '', disabled: true }),
      secondaryPhone: new FormControl({ value: '', disabled: true }),
      region: new FormControl({ value: '', disabled: true }),
      product: new FormControl({ value: '', disabled: true }),
      model: new FormControl({ value: '', disabled: true }),
      street: new FormControl({ value: '', disabled: true }),
      address: new FormControl({ value: '', disabled: true }),
      complaint: new FormControl({ value: '', disabled: true }),

      report: new FormControl({ value: '', disabled: true }),
      employee: new FormControl({ value: '', disabled: true }),
      category: new FormControl({ value: '', disabled: true }),
    });

    this.selectFieldKeys.push('region');
    this.selectFieldKeys.push('product');
    this.selectFieldKeys.push('category');
    this.selectFieldKeys.push('employee');
    this.selectFieldKeys.push('field');

    super.ngOnInit();
  }

  ngAfterViewInit(): void {
    if (this.select2Sources) {
      this.select2Sources.categories.forEach(cat => {
        this.appendSelect2Option('category', cat.Title || cat.title, cat.Oid || cat.oid, false);
      });
      this.select2Sources.employees.forEach(emp => {
        this.appendSelect2Option('employee', emp.DisplayName || emp.displayName, emp.Oid || emp.oid, false);
      });
      this.select2Sources.fields.forEach(field => {
        this.appendSelect2Option('field', field.title || field.Title, field.name || field.Name, false);
      });
    }
    super.ngAfterViewInit();
  }

  selectedFields: string[];
  onSelect2Change(e) {
    super.onSelect2Change(e);
    if ($(e.target).attr('id') == 'field') {
      this.selectedFields = $(e.target).val();
      if (this.selectedFields) {
        Object.keys(this.formGroup.controls).forEach(key => {
          if (this.selectedFields.includes(key)) {
            this.formGroup.controls[key].enable();
          }
          else {
            this.formGroup.controls[key].setValue('');
            this.formGroup.controls[key].disable();
          }
        });
      } else {
        Object.keys(this.formGroup.controls).forEach(key => {
          this.formGroup.controls[key].setValue('');
          this.formGroup.controls[key].disable();
        });
      }
    }
  }

  private select2Sources: any;
  onSidePanelOpened(e) {
    if (e.data) {
      this.select2Sources = {
        categories: e.data.categories,
        employees: e.data.employees,
      };
      if (e.data.activeTicket) {
        this.select2Sources.fields = this.fieldsSource;
      } else {
        this.select2Sources.fields = this.fieldsSource.filter(x => !x.forTicket);
      }
    }
  }

  onSubmit() {
    var order: any = {};
    var ticket: any = {};
    Object.keys(this.formGroup.value).forEach(key => {
      var field = this.fieldsSource.filter(x => x.name == key)[0];
      field.forTicket ?
        ticket[key] = this.formGroup.value[key] :
        order[key] = this.formGroup.value[key]
    });
    this.sidePanel.submitPanel({ order, ticket });
  }

  get canSubmit(): boolean {
    var valid: boolean = true;
    Object.keys(this.formGroup.value).forEach(key => {
      if (!this.formGroup.value[key]) {
        valid = false;
        return;
      }
    });
    return valid;
  }
}
