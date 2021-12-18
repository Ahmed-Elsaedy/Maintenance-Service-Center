import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditSMSMessageComponent } from '../edit-sms-message/edit-sms-message.component';
import { Direction } from '@angular/cdk/bidi/directionality';
import { ConstantPool } from '@angular/compiler';

@Component({
  selector: 'app-add-sms-message-orders',
  templateUrl: './add-sms-message-orders.component.html',
  styleUrls: ['./add-sms-message-orders.component.css']
})
export class AddSmsMessageOrdersComponent implements OnInit {

  formGroup: FormGroup;
  dir: Direction;
  orders: number[] = [];
  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditSMSMessageComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.formGroup = this.fb.group({
      
      ordersIds: new FormControl('', [Validators.required]),
      ordersIdsString: new FormControl('', [Validators.required])
    });
    this.formGroup.get("ordersIdsString").valueChanges.subscribe((x: string) => {
      var values = x.split(';').map(v => parseInt(v));
      this.orders = values.filter(f => !isNaN(f));
      this.formGroup.get("ordersIds").setValue(values.join(', '));
    });
  }

  onSubmit() {
    if (this.formGroup.valid) {
      this.dialogRef.close(this.orders.join(';'));
    }
  }

}
