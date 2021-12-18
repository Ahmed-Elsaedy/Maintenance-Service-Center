import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-orders-sms-over-time',
  templateUrl: './orders-sms-over-time.component.html',
  styleUrls: ['./orders-sms-over-time.component.css']
})
export class OrdersSmsOverTimeComponent implements OnInit {

  @Input() SetLabel: string;
  @Input() IsMonths: boolean;
  @Input() Count: number;

  public lineChartData: ChartDataSets[];
  public lineChartLabels: Label[];

  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      // borderColor: 'black',
      backgroundColor: 'rgba(160, 196, 255,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor(private dashboardClient: DashboardClient) { }

  ngOnInit() {
    this.dashboardClient.orderSMSRateOverTime(this.IsMonths, this.Count).subscribe(res => {
      this.lineChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.lineChartLabels = res.labels;
    });
  }

}
