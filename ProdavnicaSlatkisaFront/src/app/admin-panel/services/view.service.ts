import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Proizvod } from '../../models/api-models/proizvod.model';
import { Proizvodjac } from '../../models/api-models/proizvodjac.model';
import { TipProizvoda } from '../../models/api-models/tipproizvoda.model';
import { Racun } from 'src/app/models/api-models/racun.model';
import { UpdateProizvodjacRequest } from 'src/app/models/api-models/update-proizvodjac-request.model';
import { UpdateTipProizvodumRequest } from 'src/app/models/api-models/update-tipproizvodum-request.model';
import { Kupac } from 'src/app/models/api-models/kupac.model';
import { UpdateProizvodRequest } from 'src/app/models/api-models/update-proizvod-request.model';

@Injectable({
  providedIn: 'root'
})
export class ViewService {


  private baseUrl='https://localhost:44384';
  constructor(private httpClient: HttpClient) { }


  getProizvodi(): Observable<Proizvod[]>{
    return this.httpClient.get<Proizvod[]>(this.baseUrl+'/api/Proizvod')
  }
  getProizvod(idproizvoda:number): Observable<Proizvod> {
    return this.httpClient.get<Proizvod>(this.baseUrl+'/api/Proizvod/'+ idproizvoda)
  }
  addProizvod(addProizvodRequest: Proizvod): Observable<any> {
    return this.httpClient.post(this.baseUrl + '/api/Proizvod', addProizvodRequest);
  }
  updateProizvod(idproizvoda:number, proizvodRequest: Proizvod):Observable<Proizvod>{
    const updateProizvodRequest:UpdateProizvodRequest={


      cena:proizvodRequest.cena,
      idproizvodjaca:proizvodRequest.idproizvodjaca,
      tipProizvodaId:proizvodRequest.tipProizvodaId,
      kolicinaNaStanju:proizvodRequest.kolicinaNaStanju,
      naziv: proizvodRequest.naziv

    }

    return this.httpClient.put<Proizvod>(this.baseUrl+'/api/Proizvod/'+idproizvoda, updateProizvodRequest);

  }
  deleteProizvod(idproizvoda:number):Observable<Proizvod>{
    return this.httpClient.delete<Proizvod>(this.baseUrl+'/api/Proizvod/'+idproizvoda);
  }

  getProizvodjacs(): Observable<Proizvodjac[]>{
    return this.httpClient.get<Proizvodjac[]>(this.baseUrl+'/api/Proizvodjac')
  }
  getProizvodjac(idproizvodjaca:number): Observable<Proizvodjac> {
    return this.httpClient.get<Proizvodjac>(this.baseUrl+'/api/Proizvodjac/'+ idproizvodjaca)
  }
  addProizvodjac(addProizvodjacRequest: Proizvodjac): Observable<any> {
    return this.httpClient.post(this.baseUrl + '/api/Proizvodjac', addProizvodjacRequest);
  }
  updateProizvodjac(idproizvodjaca:number, proizvodjacRequest: Proizvodjac):Observable<Proizvodjac>{
    const updateProizvodjacRequest:UpdateProizvodjacRequest={

      nazivProizvodjaca:proizvodjacRequest.nazivProizvodjaca,
      zemljaPorekla:proizvodjacRequest.zemljaPorekla

    }

    return this.httpClient.put<Proizvodjac>(this.baseUrl+'/api/Proizvodjac/'+idproizvodjaca, updateProizvodjacRequest);
  }
  deleteProizvodjac(idproizvodjaca:number):Observable<Proizvodjac>{
    return this.httpClient.delete<Proizvodjac>(this.baseUrl+'/api/Proizvodjac/'+idproizvodjaca);
  }


  getaTipProizvods(): Observable<TipProizvoda[]>{
    return this.httpClient.get<TipProizvoda[]>(this.baseUrl+'/api/TipProizvodum')
  }
  getTipProizvoda(tipProizvodaId:number): Observable<TipProizvoda> {
    return this.httpClient.get<TipProizvoda>(this.baseUrl+'/api/TipProizvodum/'+ tipProizvodaId)
  }
  addTipProizvoda(addTipProizvodumRequest: TipProizvoda): Observable<any> {
    return this.httpClient.post(this.baseUrl + '/api/TipProizvodum', addTipProizvodumRequest);
  }
  updateTipProizvoda(tipProizvodaId:number, tipProizvodumRequest: TipProizvoda):Observable<TipProizvoda>{
    const updateTipProizvodumRequest:UpdateTipProizvodumRequest={

      sastav:tipProizvodumRequest.sastav

    }

    return this.httpClient.put<TipProizvoda>(this.baseUrl+'/api/TipProizvodum/'+tipProizvodaId, updateTipProizvodumRequest);
  }
  deleteTipProizvoda(tipProizvodaId:number):Observable<TipProizvoda>{
    return this.httpClient.delete<TipProizvoda>(this.baseUrl+'/api/TipProizvodum/'+tipProizvodaId);
  }


  getRacuns(): Observable<Racun[]>{
    return this.httpClient.get<Racun[]>(this.baseUrl+'/api/Racun')
  }
  getRacun(racunId:number): Observable<Racun> {
    return this.httpClient.get<Racun>(this.baseUrl+'/api/Racun/RacunId/'+ racunId)
  }
  addRacun(addRacunRequest: Racun): Observable<any> {
    return this.httpClient.post(this.baseUrl + '/api/Racun', addRacunRequest);
  }


  getKupacs(): Observable<Kupac[]>{
    return this.httpClient.get<Kupac[]>(this.baseUrl+'/api/Kupac')
  }
}
