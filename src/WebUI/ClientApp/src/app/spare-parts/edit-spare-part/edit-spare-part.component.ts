import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Direction } from '@angular/cdk/bidi';
import { EmployeeDto, SearchStoreDto } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-edit-spare-part',
  templateUrl: './edit-spare-part.component.html',
  styleUrls: ['./edit-spare-part.component.css']
})
export class EditSparePartComponent implements OnInit {

  stores: SearchStoreDto[];
  formGroup: FormGroup;
  dir: Direction;
  editMode: boolean;

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditSparePartComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    if (this.editMode) {
      this.formGroup = this.fb.group({
        sparePartId: '',
        title: new FormControl('', [Validators.required]),
        description: new FormControl(''),
        barcode: new FormControl('', [Validators.required])
      });
    } else {
      this.formGroup = this.fb.group({
        sparePartId: '',
        title: new FormControl('', [Validators.required]),
        description: new FormControl(''),
        barcode: new FormControl('', [Validators.required]),
        storeId: new FormControl(this.stores.length == 1 ? this.stores[0].storeId : '', [Validators.required]),
        openingBalance: new FormControl(null, [Validators.required, Validators.min(0)])
      });
    }
    this.formGroup.patchValue(this.data);
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }

}
