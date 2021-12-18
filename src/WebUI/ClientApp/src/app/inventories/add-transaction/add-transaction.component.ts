import { Component, OnInit, Inject } from '@angular/core';
import { SearchInventoryDto, ValueGroupDto, SearchStoreDto } from 'src/app/ElarabyCA-api';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Direction } from '@angular/cdk/bidi/directionality';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditSparePartComponent } from 'src/app/spare-parts/edit-spare-part/edit-spare-part.component';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  inventory: SearchInventoryDto;
  transactionTypes: ValueGroupDto[];
  referenceTypes: ValueGroupDto[];
  formGroup: FormGroup;
  dir: Direction;
  stores: SearchStoreDto[];

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditSparePartComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');

    this.formGroup = this.fb.group({
      inventoryId: new FormControl(this.inventory.inventoryId),
      storeTitle: new FormControl({ value: this.inventory.storeTitle, disabled: true }),
      sparePartTitle: new FormControl({ value: this.inventory.sparePartTitle, disabled: true }),
      type: new FormControl('', [Validators.required]),
      storeId: new FormControl(null),
      amount: new FormControl('', [Validators.required, Validators.min(1)]),
      description: new FormControl('', [Validators.required]),
      referenceType: new FormControl(null),
      referenceId: new FormControl(null)
    });
    this.formGroup.patchValue(this.data);

    this.formGroup.get('type').valueChanges.subscribe(c => {
      var storeId = this.formGroup.get('storeId');
      if (c == 3)
        storeId.setValidators([Validators.required]);
      else
        this.formGroup.get('storeId').clearValidators();
      storeId.updateValueAndValidity();
    });
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }
}
