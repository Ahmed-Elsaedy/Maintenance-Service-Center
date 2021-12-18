import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

const httpOptions = { headers: new HttpHeaders().set('Content-Type', 'application/json') };

@Injectable({
  providedIn: 'root'
})
export class DataApiService {
  controller: string = '';
  table: string = '';
  apiUrl: string = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.apiUrl = baseUrl + 'api/';
  }

  dataTable(data) {
    return this.http.post(`${this.apiUrl}DataTable/${this.table}`, data, httpOptions);
  }

  create(data) {
    return this.http.post(`${this.apiUrl}${this.controller}/Create`, data, httpOptions) as Observable<any>
  }

  update(id, order) {
    return this.http.put(`${this.apiUrl}${this.controller}/Update?id=${id}`, order, httpOptions) as Observable<any>;
  }

  delete(id) {
    return this.http.delete(`${this.apiUrl}${this.controller}/Delete/` + id, httpOptions) as Observable<any>;
  }

  patch(data) {
    return this.http.put(`${this.apiUrl}${this.controller}/Patch`, data, httpOptions) as Observable<any>;
  }

  query(query) {
    return this.http.post(`${this.apiUrl}${this.controller}/Query`, query, httpOptions) as Observable<any>
  }

}
