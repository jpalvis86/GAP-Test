import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

import { Insurance } from '../models/insurance';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class InsuranceService {
  private url: string = environment.apiUrl + '/api/insurances';

  constructor(private http: HttpClient) {}

  getInsurances(): Observable<Insurance[]> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
      }),
    };

    return this.http.get<Insurance[]>(this.url, httpOptions);
  }
}
