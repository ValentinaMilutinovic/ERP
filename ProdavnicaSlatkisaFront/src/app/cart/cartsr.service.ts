import { Injectable } from '@angular/core';
import { ProizvodUKorpi } from '../models/api-models/proizvodukorpi';
import { Observable, map, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Korpa } from '../models/api-models/korpa.model';
import { UpdateProiUKo } from '../models/api-models/update.proizvoduk.model';
import { AddBill, Racun } from '../models/api-models/racun.model';

@Injectable({
  providedIn: 'root'
})
export class CartsrService {


  private baseUri='https://localhost:44384/api';
  private racunId=0;
  cookieService: any;

  constructor( private httpClient:HttpClient) { }




  setRacunId(racunId: number): void {
    this.racunId = racunId;
  }

  getProductInShoppingCartByKorpaId(korpaId: number): Observable<ProizvodUKorpi[]> {
    return this.httpClient.get<ProizvodUKorpi[]>(this.baseUri + '/ProizvodUKorpi/korpa/' + korpaId);
  }

  getKorpaById(korpaId: number): Observable<Korpa> {
    return this.httpClient.get<Korpa>(this.baseUri+'/Korpa/KorpaId/'+korpaId);
  }
  updateProductQuantity(request: UpdateProiUKo,proizvodUKId:number): Observable<any> {
    return this.httpClient.put<any>(this.baseUri + '/ProizvodUKorpi/'+proizvodUKId, request);
  }
  deleteProduct(proizvodUKId: number): Observable<any> {
    return this.httpClient.delete<any>(this.baseUri + '/ProizvodUKorpi/' + proizvodUKId);
  }
  postBill(korpaId: number): Observable<number> {
    const addRacunRequest = { korpaId: korpaId }; // Create the request payload
    return this.httpClient.post<any>(this.baseUri + '/Racun', addRacunRequest)
      .pipe(
        map(response => response.racunId), // Extract the racunId from the response
        tap(racunId => {
          this.setRacunId(racunId);
        })
      );
  }
  createRacun(): Observable<any> {
    const korpaId = Number(this.cookieService.get('korpaId'));
    const body = { korpaId: korpaId };
    return this.httpClient.post<any>(this.baseUri + '/api/Racun', body);
  }
  getRacunId():number{
    return this.racunId;
  }
  getBillById(billId:number):Observable<Racun>{
    return this.httpClient.get<Racun>(this.baseUri + '/Racun/RacunId/'+billId);
  }

  createPaymentIntent(racunId:number): Observable<Racun>{
    return this.httpClient.post<Racun>(this.baseUri + '/Payment/RacunId/'+ racunId,racunId);
  }

}



