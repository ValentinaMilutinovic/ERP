import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proizvod } from '../models/ui-models/proizvod.model';
@Injectable({
  providedIn: 'root'
})
export class AdminProductService {
  private apiUrl = 'https://localhost:44384/api/Proizvod';

constructor(private http: HttpClient) {}

addProizvod(addProductRequest: Proizvod): Observable<any> {
  return this.http.post(this.apiUrl, addProductRequest);
}
}
