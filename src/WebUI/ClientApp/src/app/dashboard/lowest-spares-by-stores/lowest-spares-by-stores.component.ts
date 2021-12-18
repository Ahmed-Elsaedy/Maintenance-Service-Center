import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-lowest-spares-by-stores',
  templateUrl: './lowest-spares-by-stores.component.html',
  styleUrls: ['./lowest-spares-by-stores.component.css']
})
export class LowestSparesByStoresComponent implements OnInit {
  @Input() SetLabel: string;
  @Input() StoreIds: number[];

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

    this.dashboardClient.lowestSparesByStoreIds(this.StoreIds).subscribe(res => {
      this.barChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.barChartLabels = res.labels;
    });
  }

}
