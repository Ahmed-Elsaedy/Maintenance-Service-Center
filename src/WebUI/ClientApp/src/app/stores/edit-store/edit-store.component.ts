import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Direction } from '@angular/cdk/bidi';
import { EmployeeDto, SearchStoreDto } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-edit-store',
  templateUrl: './edit-store.component.html',
  styleUrls: ['./edit-store.component.css']
})
export class EditStoreComponent implements OnInit {

  employees: EmployeeDto[];
  formGroup: FormGroup;
  dir: Direction;

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditStoreComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.formGroup = this.fb.group({
      storeId: '',
      title: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      administrator: new FormControl('', [Validators.required])
    });
    this.formGroup.patchValue(this.data);
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }

}
