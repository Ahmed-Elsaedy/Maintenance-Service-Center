import { Direction } from '@angular/cdk/bidi/directionality';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-financial-statistics',
  templateUrl: './financial-statistics.component.html',
  styleUrls: ['./financial-statistics.component.scss']
})
export class FinancialStatisticsComponent implements OnInit {
  dir: Direction;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<FinancialStatisticsComponent>) { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
    
  }

}
