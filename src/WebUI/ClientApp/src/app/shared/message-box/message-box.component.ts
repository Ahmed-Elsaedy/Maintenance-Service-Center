import { Component, OnInit } from '@angular/core';
import { Direction } from '@angular/cdk/bidi';

export interface IMessageBoxOptions {
  title: string,
  message: string,
  isNotification?: boolean
  localize?: boolean
}

@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrls: ['./message-box.component.css']
})
export class MessageBoxComponent implements OnInit {
  dir: Direction;
  options: IMessageBoxOptions;

  constructor() { }

  ngOnInit() {
    this.dir = <Direction>document.querySelector('html').getAttribute('dir');
  }

}
