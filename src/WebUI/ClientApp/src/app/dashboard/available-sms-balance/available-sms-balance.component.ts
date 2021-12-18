import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-available-sms-balance',
  templateUrl: './available-sms-balance.component.html',
  styleUrls: ['./available-sms-balance.component.css']
})
export class AvailableSmsBalanceComponent implements OnInit {

  @Input() SetLabel: string;

  public lineChartData: ChartDataSets[];
  public lineChartLabels: Label[];

  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      // borderColor: 'black',
      backgroundColor: ['rgba(160, 196, 255,0.3)', '#ff8282'],
    }
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'pie';
  public lineChartPlugins = [];

  constructor(private dashboardClient: DashboardClient) { }

  ngOnInit() {
    this.dashboardClient.availableSmsBalance(null).subscribe(res => {
      this.lineChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.lineChartLabels = res.labels;
    });
  }

}
