import { Component, OnInit } from '@angular/core';
import { DataApiService } from '../core/services/data-api.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-technician-input',
  templateUrl: './technician-input.component.html',
  styleUrls: ['./technician-input.component.css']
})
export class TechnicianInputComponent implements OnInit {
  reports: any[] = [];

  constructor(private router: Router, private dataApi: DataApiService, private authorizeService: AuthorizeService) {
    this.dataApi.controller = "TechnicianReport";
  }

  ngOnInit() {

    this.authorizeService.getUser().subscribe((x: any) => {

      if ( x && x.roles.includes('Technician'))
        this.router.navigate(['unauthorized']);
      else {
        this.dataApi.controller = "TechnicianReport";
        var query = { select: 'Oid, Date, Report, Technician' };
        this.dataApi.query(query).subscribe(res => {
          this.reports = res;
        });
      }
    })


  }

  onDelete(oid) {
    this.dataApi.delete(oid).subscribe(x => {
      this.ngOnInit();
    });
  }

}
