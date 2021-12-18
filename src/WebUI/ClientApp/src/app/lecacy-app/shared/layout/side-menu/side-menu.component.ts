import { Component, OnInit, Input } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {

  @Input() userRoles: string;
  constructor() { }

  ngOnInit() {

  }

  onInventory(){
    window.open("http://gaboubstock.azurewebsites.net/", "_blank");
  }

}
