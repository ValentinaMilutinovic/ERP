import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proizvod } from '../models/api-models/proizvod.model';

@Injectable({
  providedIn: 'root'
})
export class ProizvodService {


  private baseUri='https://localhost:44384';
  constructor(private httpClient:HttpClient) { }

  getProizvodi():Observable<Proizvod[]>
  {
    return this.httpClient.get<Proizvod[]>(this.baseUri+'/api/Proizvod');
  }
  getProizvod(idProizvoda: number): Observable<Proizvod>{
    return this.httpClient.get<Proizvod>(this.baseUri+'/api/Proizvod/'+idProizvoda)
  }
  getCartProducts(userId: string): Observable<Proizvod[]> {
    return this.httpClient.get<Proizvod[]>(`${this.baseUri}/api/cart/${userId}/products`);
  }
  
}
