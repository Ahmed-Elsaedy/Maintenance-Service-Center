import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';
import ChartDataLabels from 'chartjs-plugin-datalabels';

@Component({
  selector: 'app-orders-by-category',
  templateUrl: './orders-by-category.component.html',
  styleUrls: ['./orders-by-category.component.css']
})
export class OrdersByCategoryComponent implements OnInit {
  @Input() SetLabel: string;
  @Input() Months: number;
  @Input() Days: number;

  public barChartOptions: ChartOptions = {
    responsive: true,
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [ChartDataLabels];

  public barChartLabels: Label[];
  public barChartData: ChartDataSets[];

  constructor(private dashboardClient: DashboardClient) { }

  ngOnInit() {

    this.dashboardClient.ordersByCategory(this.Months, this.Days).subscribe(res => {
      this.barChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.barChartLabels = res.labels;
    });
  }

}
