import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

import { Customer } from '../models/customer';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private url: string = environment.apiUrl + '/api/customers';

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
      }),
    };

    return this.http.get<Customer[]>(this.url, httpOptions);
  }
}
