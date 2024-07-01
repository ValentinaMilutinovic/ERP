import { Korpa } from './../models/ui-models/korpa.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import * as moment from 'moment';
import { tap, shareReplay, map, catchError, switchMap } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Kupac } from '../models/ui-models/kupac.model';
import { Administrator } from '../models/ui-models/administrator.model';


@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {
  private loginUrl = 'https://localhost:44384/api/Kupac/login'; // Update with your URL
  private usernamerkUrl='https://localhost:44384/api/Kupac/UsernameRk';
  private korpaUrl='https://localhost:44384/api/Korpa';
  private loggedInUsername: string = '';
  private kupacId: number = 0;
  private korpaId: number=0;
  private adminID=0;
  private loginAdminUrl = 'https://localhost:44384/api/Administrator/login';
  private usernamerkAdminUrl='https://localhost:44384/api/dministrator/Username'


  constructor(private httpClient: HttpClient, private cookieService: CookieService) {
    this.checkKorpaId();

  }
  setToken(token: string): void {
    const d = new Date();
  d.setTime(d.getTime() + (1 * 60 * 60 * 1000));
    this.cookieService.set('token', token, { expires: d });
  }

  getToken(): string | null {
    return this.cookieService.get('token');
  }



  login(email: string, password: string): Observable<any> {
    const body = {
      email,
      password

    };

    this.setUsername(email);
    return this.httpClient.post(this.loginUrl, body)
    .pipe(
      tap(res => this.setSession(res)),
      shareReplay()
    );

  }

  loginAdmin(email: string, password: string): Observable<any> {
    const body = {
      email,
      password

    };

    this.setUsername(email);
    return this.httpClient.post(this.loginAdminUrl, body)
    .pipe(
      tap(res => this.setSession(res)),
      shareReplay()
    );

  }


  private setSession(authResult: any) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');

    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()));
  }

  getAdminExpiration() {
    const expiration = localStorage.getItem('expires_at');
  const expiresAt = expiration ? moment(JSON.parse(expiration)) : null;
  return expiresAt;
  }
  private fetchAdminId(): Observable<number> {
    const Username = this.loggedInUsername;

    return this.httpClient.get<Administrator>(this.usernamerkAdminUrl +'/'+  Username)
    .pipe(
      map((response: Administrator) =>{
        if (response && response.adminID) {
          return response.adminID;
        } else {
          throw new Error('Invalid response or kupacId not found');
        }
      }),
      catchError((error: any) => {
        console.error('Error fetching kupacId:', error);
        return throwError(error);  // Return zero if an error occurs
      })
    );
  }









  isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem('expires_at');
  const expiresAt = expiration ? moment(JSON.parse(expiration)) : null;
  return expiresAt;
  }
  private fetchKupacId(): Observable<number> {
    const UsernameRk = this.loggedInUsername;

    return this.httpClient.get<Kupac>(this.usernamerkUrl +'/'+  UsernameRk)
    .pipe(
      map((response: Kupac) =>{
        if (response && response.kupacId) {
          return response.kupacId;
        } else {
          throw new Error('Invalid response or kupacId not found');
        }
      }),
      catchError((error: any) => {
        console.error('Error fetching kupacId:', error);
        return throwError(error);  // Return zero if an error occurs
      })
    );
  }
  getUsername(): string {
    return this.loggedInUsername;
  }

  setUsername(username: string): void {
    this.loggedInUsername = username;
  }
  createKorpa(): Observable<any> {
    return this.fetchKupacId()
    .pipe(
      switchMap((kupacId: number) => {
        this.kupacId = kupacId;
        if (this.kupacId === 0) {
          throw new Error('Invalid kupacId');
        }
        const korpaRequest: Korpa = new Korpa(this.kupacId);
        return this.httpClient.post(this.korpaUrl, korpaRequest);
      }),
      catchError((error: any) => {
        console.error("Error creating korpa:", error);
        return throwError(error);
      })
    );
  }

  getKupacId(): number {
    return this.kupacId;
  }

  getAdminId(): number {
    return this.adminID;
  }

  getKorpaId(): number {
    return Number(this.cookieService.get('korpaId'));
  }

  setKorpaId(korpaId: number): void {
    this.korpaId = korpaId;
    const d = new Date();
    d.setTime(d.getTime() + (1 * 60 * 60 * 1000));
    this.cookieService.set('korpaId', korpaId.toString(), { expires: d });
  }
  logout() {
  this.cookieService.delete('token');
  this.cookieService.delete('korpaId');
  this.clearLocalStorage();
  this.clearKupacId();
  this.clearAdminId();
  console.log('izlogovan korisnik')
  }

private clearLocalStorage() {
  localStorage.removeItem('id_token');
  localStorage.removeItem('expires_at');
  localStorage.removeItem('korisnik_id');
    localStorage.removeItem('korpaId')
  }

private clearAdminId(){
  this.adminID=0;
}


private clearKupacId() {
  this.kupacId = 0;
  this.korpaId=0;
  }
  private checkKorpaId(): void {
    const korpaId = this.cookieService.get('korpaId');
    if (korpaId) {
      this.korpaId = Number(korpaId);
    }
  }

}
