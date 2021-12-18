import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IndexWorkOrder } from '../models/elaraby/IndexWorkOrder';
declare var $: any;

const httpOptions = { headers: new HttpHeaders().set('Content-Type', 'application/json') };


@Injectable({
  providedIn: 'root'
})
export class ElarabyApiService {


  private apiUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.apiUrl = baseUrl + 'api/Elaraby';
  }

  getWorkOrders() {
    return this.http.get(this.apiUrl + "/WorkOrders", { responseType: 'text' })
      .pipe(map(html => {
        var workOrders: IndexWorkOrder[] = [];
        var trs = $(html).find('tr:not(:first)').each((index, tr) => {
          var tds = $(tr).find('td');
          var order = new IndexWorkOrder();
          order.OrderNumber = tds[1].innerText.trim();
          order.CustomerName = tds[2].innerText.trim();
          order.AreaName = tds[3].innerText.trim();
          order.LandLine = tds[4].innerText.trim();
          order.Address = tds[5].innerText.trim();
          order.ProductType = tds[6].innerText.trim();
          order.ProblemCliet = tds[7].innerText.trim();
          order.MobileNumber = tds[8].innerText.trim();
          order.ScheduledStartDate = tds[9].innerText.trim();
          order.BackOfficeNotes = tds[10].innerText.trim();
          order.WorkOrderType = tds[11].innerText.trim();
          order.HasComplaint = tds[12].innerText.trim();
          order.SapNumber = tds[13].innerText.trim();

          var mappingInput = $(tds[14]).find('input');
          order.WorkOrderId = mappingInput.val();
          order.Oid = mappingInput.attr('oid');

          workOrders.push(order);
        });
        return { data: workOrders }

      })) as Observable<any>;
  }

  download(data: any) {
    return this.http.post(this.apiUrl + "/Create" , data, httpOptions) as Observable<any>
  }
}
