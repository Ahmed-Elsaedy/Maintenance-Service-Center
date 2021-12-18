import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Direction } from '@angular/cdk/bidi';
import { EmployeeDto } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-edit-sms-message',
  templateUrl: './edit-sms-message.component.html',
  styleUrls: ['./edit-sms-message.component.css']
})
export class EditSMSMessageComponent implements OnInit {

  formGroup: FormGroup;
  dir: Direction;

  constructor(private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditSMSMessageComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    this.formGroup = this.fb.group({
      id: '',
      title: new FormControl('', [Validators.required]),
      text: new FormControl('', [Validators.required, Validators.maxLength(70)])
    });
    this.formGroup.patchValue(this.data);
  }

  onSubmit() {
    if (this.formGroup.valid)
      this.dialogRef.close(this.formGroup.value);
  }

}
