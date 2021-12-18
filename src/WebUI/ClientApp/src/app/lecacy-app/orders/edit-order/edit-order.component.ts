import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { SidePanelService } from 'src/app/lecacy-app/core/services/side-panel.service';
import { EditComponentBase } from 'src/app/lecacy-app/core/abstract/edit-component.base';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css']
})
export class EditOrderComponent extends EditComponentBase {

  constructor(sidePanel: SidePanelService,
    private fb: FormBuilder) {
    super(sidePanel);
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      oid: new FormControl(''),
      orderid: new FormControl(''),
      dateAssigned: new FormControl({ value: '' }),
      customer: new FormControl('', [Validators.required]),
      primaryPhone: new FormControl('', [Validators.required]),
      secondaryPhone: new FormControl(''),
      region: new FormControl('', [Validators.required]),
      product: new FormControl('', [Validators.required]),
      model: new FormControl(''),
      street: new FormControl(''),
      address: new FormControl('', [Validators.required]),
      complaint: new FormControl('', [Validators.required]),
      activeTicket: new FormControl(null),
      workOrderId: new FormControl(null),
      sapNumber: new FormControl(null),
      caseNumber: new FormControl(null)
    });

    this.selectFieldKeys.push('region');
    this.selectFieldKeys.push('product');

    super.ngOnInit();
  }

  onSidePanelOpened(e) {
    this.formGroup.patchValue(e.data);
  }

  onValidSubmit() {
    this.formGroup.value.caseNumber = this.formGroup.value.orderid;
    this.sidePanel.submitPanel(this.formGroup.value);
  }
}
