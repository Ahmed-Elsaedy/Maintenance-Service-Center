import { Component, OnInit } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent extends EditComponentBase {
  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel)
  }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      oid: new FormControl(''),
      displayName: new FormControl('', [Validators.required]),
      primaryPhone: new FormControl('', [Validators.required]),
      secondaryPhone: new FormControl('')
    });
    super.ngOnInit();
  }

  onSidePanelOpened(e) {
    if (e.data)
      this.formGroup.patchValue(e.data);
  }

  onValidSubmit() {
    var viewModel = this.formGroup.value;
    this.sidePanel.submitPanel(viewModel);
  }
}
