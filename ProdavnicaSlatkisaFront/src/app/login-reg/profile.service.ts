import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ReplaySubject, Observable, tap, catchError, of } from 'rxjs';
import jwtDecode from 'jwt-decode';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Kupac } from '../models/ui-models/kupac.model';
import { ShowKorpa } from '../models/api-models/korpa.model';
import { LoginServiceService } from '../services/login-service.service';
import { Administrator } from '../models/ui-models/administrator.model';
@Injectable({
  providedIn: 'root'
})
export class ProfileService{
   private baseUri = 'https://localhost:44384';

private currentKupacSource = new ReplaySubject<Kupac | null>(1);
currentKorisnKupac$ = this.currentKupacSource.asObservable();

private currentAdminSource = new ReplaySubject<Administrator | null>(1);
currentAdmin$ = this.currentAdminSource.asObservable();

private currentKorpaCartSource = new ReplaySubject<ShowKorpa | null>(1);
currentKorpaCartSource$ = this.currentKorpaCartSource.asObservable();

private uloga='';

constructor(private http: HttpClient, private router: Router, private cookieService: CookieService,private jwtHelper: JwtHelperService,  private loginServiceService:LoginServiceService) {
  this.checkToken();
  this.checkKorpa();
  this.checkAdminToken();
}

getDecodedAccessToken(token: string): any {
  try {
    return jwtDecode(token);
  } catch(Error) {
    return null;
  }
}
public getUloga(){

  return this.uloga;
}

private checkToken() {
  const token = this.cookieService.get('token');
  if (token) {
    const decodedToken = this.getDecodedAccessToken(token);
    if (decodedToken) {
      const UsernameRk = Object.values(decodedToken)[0] as string;
      this.uloga=Object.values(decodedToken)[1] as string;
      if (UsernameRk) {
        this.loadCurrentKupac(token, UsernameRk).subscribe();
      } else {
        // Invalid token format, clear the cookie and reset the current user
        this.cookieService.delete('token');
        this.currentKupacSource.next(null);
      }
    } else {
      // Invalid token, clear the cookie and reset the current user
      this.cookieService.delete('token');
      this.currentKupacSource.next(null);
    }
  } else {
    this.currentKupacSource.next(null);
  }
}
private checkKorpa(){
  const korpaId = Number(this.cookieService.get('korpaId'));
  if(korpaId){
    this.loadCurrentKorpa(korpaId).subscribe();
  }
}


private checkAdminToken() {
  const token = this.cookieService.get('token');
  if (token) {
    const decodedToken = this.getDecodedAccessToken(token);
    if (decodedToken) {
      const Username = Object.values(decodedToken)[0] as string;
      this.uloga=Object.values(decodedToken)[1] as string;
      if (Username) {
        this.loadCurrentAdmin(token, Username).subscribe();
      } else {
        // Invalid token format, clear the cookie and reset the current user
        this.cookieService.delete('token');
        this.currentAdminSource.next(null);
      }
    } else {
      // Invalid token, clear the cookie and reset the current user
      this.cookieService.delete('token');
      this.currentAdminSource.next(null);
    }
  } else {
    this.currentAdminSource.next(null);
  }
}

loadCurrentKorpa(korpaId: number): Observable<ShowKorpa | null> {
  return this.http.get<ShowKorpa>(this.baseUri + '/api/Korpa/KorpaId/' + korpaId).pipe(
    tap((korpa: ShowKorpa) => {
      this.cookieService.set('korpaId', korpaId.toString(), { expires: 1 });
      localStorage.setItem('korpaId', korpa.korpaId.toString());
      this.currentKorpaCartSource.next(korpa);
    }),
    catchError((error: any) => {
      console.error('Error:', error);
      this.cookieService.delete('korpaId'); // Remove the korpaId cookie
      localStorage.removeItem('korpaId');
      this.currentKorpaCartSource.next(null);
      return of(null);
    })
  );
}
loadCurrentKupac(token: string, username: string): Observable<Kupac | null> {
  // Assuming you have a backend endpoint to retrieve the user by username
  return this.http.get<Kupac>(this.baseUri + '/api/Kupac/UsernameRk/' + username).pipe(
    tap((kupac: Kupac) => {
      this.cookieService.set('token', token, { expires: 1 }); // Set token as a cookie with 1-day expiration
      localStorage.setItem('kupacId', kupac.kupacId.toString());
      this.currentKupacSource.next(kupac);
    }),
    catchError((error: any) => {
      console.error('Error:', error);
      this.cookieService.delete('token'); // Remove the token cookie
      localStorage.removeItem('kupacId');
      this.currentKupacSource.next(null);
      return of(null);
    })
  );
}


loadCurrentAdmin(token: string, username: string): Observable<Administrator | null> {
  // Assuming you have a backend endpoint to retrieve the user by username
  return this.http.get<Administrator>(this.baseUri + '/api/Administrator/Username/' + username).pipe(
    tap((admin: Administrator) => {
      this.cookieService.set('token', token, { expires: 1 }); // Set token as a cookie with 1-day expiration
      localStorage.setItem('adminID', admin.adminID.toString());
      this.currentAdminSource.next(admin);
    }),
    catchError((error: any) => {
      console.error('Error:', error);
      this.cookieService.delete('token'); // Remove the token cookie
      localStorage.removeItem('adminID');
      this.currentAdminSource.next(null);
      return of(null);
    })
  );
}


}

