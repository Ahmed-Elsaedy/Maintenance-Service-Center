import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import {
  CookieService
} from 'ngx-cookie-service';
import {
  TranslateService
} from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private translate: TranslateService,) { }
  ngOnInit() { }

}
