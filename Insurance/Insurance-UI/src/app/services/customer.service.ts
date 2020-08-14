import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

import { Customer } from '../models/customer';
import { CustomerInsurance } from '../models/customerinsurance';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private url: string = environment.apiUrl + '/api/customers';

  private httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
    }),
  };
  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.url, this.httpOptions);
  }

  getCustomer(customerId: number): Observable<CustomerInsurance> {
    return this.http.get<CustomerInsurance>(
      this.url + '/' + customerId,
      this.httpOptions
    );
  }

  updateCustomers(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.url, customer, this.httpOptions);
  }
}
