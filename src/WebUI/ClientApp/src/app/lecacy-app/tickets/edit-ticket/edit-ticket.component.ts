import { Component } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-ticket',
  templateUrl: './edit-ticket.component.html',
  styleUrls: ['./edit-ticket.component.css']
})
export class EditTicketComponent extends EditComponentBase {

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel);
  }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      oid: new FormControl(''),
      order: new FormControl('', [Validators.required]),
      date: new FormControl('', [Validators.required]),
      report: new FormControl('', [Validators.required]),
      employee: new FormControl(null, [Validators.required]),
      category: new FormControl(null, [Validators.required]),
      isActive: new FormControl('')
    });

    this.selectFieldKeys.push('category');
    this.selectFieldKeys.push('employee');

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
    }
    super.ngAfterViewInit();
  }

  private select2Sources: any;
  onSidePanelOpened(e) {
    if (e.data) {
      this.select2Sources = {
        categories: e.data.categories,
        employees: e.data.employees
      };
      this.formGroup.patchValue(e.data.viewModel);
    }
  }

  onValidSubmit() {
    var viewModel = this.formGroup.value;
    if (viewModel.employee)
      viewModel.employee = parseInt(viewModel.employee);
    if (viewModel.category)
      viewModel.category = parseInt(viewModel.category);
    viewModel.isActive = viewModel.isActive ? true : false;
    this.sidePanel.submitPanel(viewModel);
  }
}
