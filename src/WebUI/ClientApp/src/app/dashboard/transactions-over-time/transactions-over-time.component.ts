import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-transactions-over-time',
  templateUrl: './transactions-over-time.component.html',
  styleUrls: ['./transactions-over-time.component.css']
})
export class TransactionsOverTimeComponent implements OnInit {
  @Input() StoreIds: number[];
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
    }
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor(private dashboardClient: DashboardClient) { }

  ngOnInit() {
    this.dashboardClient.transactionsRateOverTime(this.StoreIds, this.IsMonths, this.Count).subscribe(res => {
      this.lineChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.lineChartLabels = res.labels;
    });
  }

}
