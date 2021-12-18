import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, ValidatorFn, ValidationErrors } from '@angular/forms';
import { Direction } from '@angular/cdk/bidi/directionality';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditSMSMessageComponent } from '../edit-sms-message/edit-sms-message.component';

export function isValidPhoneNumber(val): boolean {
  var isNumber = !isNaN(parseFloat(val)) && isFinite(val);
  if (!isNumber) return false;
  else if (val.length != 12) return false;
  //else if (val.length != 10 && val.length != 11 && val.length != 12) return false;
  //else if (val.length == 10 && !val.startsWith('1')) return false;
  //else if (val.length == 11 && !val.startsWith('0')) return false;
  else if (val.length == 12 && !val.startsWith('2')) return false;
  else return true;
}

@Component({
  selector: 'app-edit-order-phones',
  templateUrl: './edit-order-phones.component.html',
  styleUrls: ['./edit-order-phones.component.css']
})
export class EditOrderPhonesComponent implements OnInit {

  formGroup: FormGroup;
  dir: Direction;

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditSMSMessageComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.formGroup = this.fb.group({
      orderId: new FormControl('', [Validators.required]),
      primaryPhone: new FormControl(''),
      secondaryPhone: new FormControl('')
    });
    this.formGroup.setValidators(this.phoneNumbersValidator());
    this.formGroup.patchValue(this.data);
  }

  phoneNumbersValidator(): ValidatorFn {
    return (g: FormGroup): ValidationErrors => {
      var primaryPhone = g.controls.primaryPhone.value;
      var secondaryPhone = g.controls.secondaryPhone.value;

      if (!primaryPhone && !secondaryPhone)
        return { invalid: '' }
      else if (primaryPhone && isValidPhoneNumber(primaryPhone))
        return null;
      else if (secondaryPhone && isValidPhoneNumber(secondaryPhone))
        return null;
      else
        return { invalid: '' }
    }
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }

}
