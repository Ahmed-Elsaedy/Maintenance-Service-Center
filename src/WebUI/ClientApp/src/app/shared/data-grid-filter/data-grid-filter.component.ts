import { Component, OnInit } from '@angular/core';

export interface IDataGridFilterOptions {
  columns: { name: string, title: string, type: string, values?: { value: string, text: string }[] }[]
}

@Component({
  selector: 'app-data-grid-filter',
  templateUrl: './data-grid-filter.component.html',
  styleUrls: ['./data-grid-filter.component.css']
})
export class DataGridFilterComponent implements OnInit {

  options: IDataGridFilterOptions;
  filters: { }[] = [];

  constructor() { }

  ngOnInit() {
    this.options = {
      columns: [
        { name: 'firstname', title: 'الاسم الاول', type: 'string' },
        { name: 'secondname', title: 'الاسم الثاني', type: 'string' },
        { name: 'age', title: 'العمر', type: 'number' }
      ]
    }
  }

  onRemoveChip(col) {

  }

}
