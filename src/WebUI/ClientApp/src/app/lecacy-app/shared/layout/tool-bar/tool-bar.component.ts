import { Component, OnInit } from '@angular/core';
import { ToolbarService } from 'src/app/lecacy-app/core/services/toolbar.service';

@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent implements OnInit {

  constructor(public toolbarService: ToolbarService) { }

  ngOnInit() {
  }

  onToolbarActionClick(data, index) {
    this.toolbarService.actions[index].execute(index);
  }

}
