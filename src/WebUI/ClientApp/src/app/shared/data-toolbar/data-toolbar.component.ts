import { Component, OnInit, Input, Output } from '@angular/core';

export interface IDataToolbarOptions {
  icon: string,
  title: string,
  subTitle?: string,
  actions: { id: number, title: string, icon?: string, disabled?: boolean, execute?: ()=> void }[],
  localize?: boolean
}

@Component({
  selector: 'app-data-toolbar',
  templateUrl: './data-toolbar.component.html',
  styleUrls: ['./data-toolbar.component.css']
})
export class DataToolbarComponent implements OnInit {

  @Input() options: IDataToolbarOptions;

  constructor() { }

  ngOnInit() {
  }

}
