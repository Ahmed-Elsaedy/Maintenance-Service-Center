import { Component, OnInit, Inject } from '@angular/core';
import { SearchInventoryDto, ValueGroupDto, SearchStoreDto, EmployeeDto } from 'src/app/ElarabyCA-api';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Direction } from '@angular/cdk/bidi/directionality';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditSparePartComponent } from 'src/app/spare-parts/edit-spare-part/edit-spare-part.component';

@Component({
  selector: 'app-edit-financial-transaction',
  templateUrl: './edit-financial-transaction.component.html',
  styleUrls: ['./edit-financial-transaction.component.css']
})
export class AddFinancialTransactionComponent implements OnInit {

  transactionId: any;
  transactionTypes: ValueGroupDto[];
  formGroup: FormGroup;
  dir: Direction;
  employees: EmployeeDto[];

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<AddFinancialTransactionComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.formGroup = this.fb.group({
      transactionId: new FormControl(),
      employeeId: new FormControl(null, [Validators.required]),
      title: new FormControl(null, [Validators.required]),
      type: new FormControl(null, [Validators.required]),
      amount: new FormControl(null, [Validators.required, Validators.min(1)]),
      remarks: new FormControl(),
      date: new FormControl(new Date(), [Validators.required]),
    });
    this.formGroup.patchValue(this.data);
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }
}
