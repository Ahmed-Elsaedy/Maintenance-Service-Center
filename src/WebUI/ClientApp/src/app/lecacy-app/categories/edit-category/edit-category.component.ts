import { Component, OnInit } from '@angular/core';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent extends EditComponentBase {

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel)
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      oid: new FormControl(''),
      title: new FormControl('', [Validators.required]),
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
