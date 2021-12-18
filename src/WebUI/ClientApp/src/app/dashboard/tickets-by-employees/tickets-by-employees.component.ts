import { Component, Input, OnInit } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { DashboardClient } from 'src/app/ElarabyCA-api';

@Component({
  selector: 'app-tickets-by-employees',
  templateUrl: './tickets-by-employees.component.html',
  styleUrls: ['./tickets-by-employees.component.css']
})
export class TicketsByEmployeesComponent implements OnInit {

  @Input() SetLabel: string;
  @Input() Months: number;
  @Input() Days: number;

  public lineChartData: ChartDataSets[];
  public lineChartLabels: Label[];

  public lineChartOptions: ChartOptions = {
    responsive: true,
  };
  public lineChartColors: Color[] = [
    {
      // borderColor: 'black',
      backgroundColor: ['rgba(160, 196, 255,0.3)'],
    }
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'bar';
  public lineChartPlugins = [];

  constructor(private dashboardClient: DashboardClient) { }

  ngOnInit() {
    this.dashboardClient.ticketsByEmployees(this.Months, this.Days).subscribe(res => {
      this.lineChartData = [
        { data: res.data, label: this.SetLabel }
      ];
      this.lineChartLabels = res.labels;
    });
  }

}
