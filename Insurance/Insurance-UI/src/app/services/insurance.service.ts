import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { environment } from '../../environments/environment';

import { Insurance } from '../models/insurance';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class InsuranceService {
  private url: string = environment.apiUrl + '/api/insurances';
  private httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
    }),
  };
  constructor(private http: HttpClient) {}

  getInsurances(): Observable<Insurance[]> {
    return this.http.get<Insurance[]>(this.url, this.httpOptions);
  }

  addInsurance(insurance: Insurance): Observable<Insurance> {
    return this.http.post<Insurance>(this.url, insurance, this.httpOptions);
  }

  updateInsurance(insurance: Insurance): Observable<Insurance> {
    return this.http.put<Insurance>(this.url, insurance, this.httpOptions);
  }

  deleteInsurance(insuranceId: number): Observable<{}> {
    return this.http.delete(
      this.url + '?insuranceId=' + insuranceId,
      this.httpOptions
    );
  }
}
