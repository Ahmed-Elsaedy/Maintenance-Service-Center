import { Component, OnInit } from '@angular/core';
import { MatListOption } from '@angular/material/list';
import { SearchStoreDto, StoresClient } from '../ElarabyCA-api';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private storesClient: StoresClient) { }

  stores: SearchStoreDto[];
  selectedStore: number[];
  isLoading: boolean;

  ngOnInit() {
    this.isLoading = true;
    this.storesClient.search(null).subscribe(res => {
      this.stores = res.data;
      this.isLoading = false;
    });
  }

  loadInventories() {
    this.isLoading = true;
    setInterval(() => {
      this.isLoading = false;
    }, 100);
  }

  storesSelectionChanged(options: MatListOption[]) {
    this.selectedStore = options.map(x => x.value);
    this.loadInventories();
  }

}
