import { Component, OnInit } from '@angular/core';
import { DataApiService } from 'src/app/lecacy-app/core/services/data-api.service';
import { ActivatedRoute } from '@angular/router';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-add-input',
  templateUrl: './add-input.component.html',
  styleUrls: ['./add-input.component.css']
})
export class AddInputComponent implements OnInit {
  currentUser: any;
  currentOrder: any;
  currentReport: any = {}
  userName: string;
  constructor(private dataApi: DataApiService, private activatedRoute: ActivatedRoute,
    private authorizeService: AuthorizeService) {
  }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(params => {
      this.dataApi.controller = "Order";
      var query = {
        select: 'Oid,Customer',
        where: {
          "Field": "Oid",
          "FilterType": "Equal",
          "Value": +params.get('oid')
        }
      }
      this.dataApi.query(query).subscribe(res => {
        if (res.length > 0) {
          this.currentOrder = res[0];
        }
      })

      this.dataApi.controller = "TechnicianReport";
      var query = {
        select: 'Oid, Report, Technician',
        where: {
          "Field": "Oid",
          "FilterType": "Equal",
          "Value": +params.get('oid')
        }
      }
      this.dataApi.query(query).subscribe(res => {
        if (res.length > 0) {
          this.currentReport = res[0];
          this.userName = this.currentReport.Technician;
        } else
          this.userName = this.currentUser.name;
      })
    });

    this.authorizeService.getUser().subscribe((obj: any) => {
      if (obj) {
        this.currentUser = obj;
      }
    });
  }

  onSubmit(form) {
    this.dataApi.controller = "TechnicianReport";
    if (this.currentReport.Oid) {
      this.dataApi.update(this.currentOrder.Oid, {
        Oid: this.currentOrder.Oid,
        Report: form.value.report,
        Technician: this.currentUser.name
      }).subscribe(x => {
        alert("success");
      })
    } else {
      this.dataApi.create({
        Oid: this.currentOrder.Oid,
        Report: form.value.report,
        Technician: this.currentUser.name
      }).subscribe(x => {
        alert("success");
      })
    }
  }


}
