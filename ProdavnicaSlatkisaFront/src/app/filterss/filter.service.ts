import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  private baseUri='https://localhost:44384';
  constructor(private httpClient:HttpClient) {
    getProizvodjac(){
      this.httpClient.get<any>(this.baseUri + '/api/Proizvodjac')
    }
  }
}
