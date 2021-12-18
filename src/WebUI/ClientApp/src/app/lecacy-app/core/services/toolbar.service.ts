import { Injectable } from '@angular/core';
import { ToolbarAction } from '../models/toolbar-action';

@Injectable({
  providedIn: 'root'
})
export class ToolbarService {
  title: string;
  actions: ToolbarAction[];

  constructor() {
    this.title = 'Title';
    this.actions = [];
  }
}
