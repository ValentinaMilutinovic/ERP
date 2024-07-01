import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ProfileService } from './login-reg/profile.service';
import { LoginServiceService } from './services/login-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private profileService: ProfileService, private cookieService: CookieService,private loginServiceService:LoginServiceService) {}

  ngOnInit(): void {
    this.initializeUser();
    this.initializeShoppingCart();
    this.initializeAdmin();
  }

  private initializeUser() {
    console.log('Initializing user...');
    const token = this.cookieService.get('token');
    if (token) {
      const decodedToken = this.profileService.getDecodedAccessToken(token);
      if (decodedToken) {
        const UsernameRk = Object.values(decodedToken)[0] as string;
        this.profileService.loadCurrentKupac(token, UsernameRk).subscribe((kupac) => {

          console.log('Logged in user:', kupac);
        });
      } else {
        // Invalid token, clear the cookie and set login status to false
        this.cookieService.delete('token');

      }
    }
  }

  private initializeAdmin() {
    console.log('Initializing admin...');
    const token = this.cookieService.get('token');
    if (token) {
      const decodedToken = this.profileService.getDecodedAccessToken(token);
      if (decodedToken) {
        const Username = Object.values(decodedToken)[0] as string;
        this.profileService.loadCurrentAdmin(token, Username).subscribe((admin) => {

          console.log('Logged in user:', admin);
        });
      } else {
        // Invalid token, clear the cookie and set login status to false
        this.cookieService.delete('token');

      }
    }
  }


  private initializeShoppingCart(){
    const korpaId = Number(this.cookieService.get('korpaId'));
    if(korpaId){
      this.profileService.loadCurrentKorpa(korpaId).subscribe((korpa)=>{
        const korpaId=korpa?.korpaId || 0;
        this.loginServiceService.setKorpaId(korpaId);
      });
    }else{
      this.cookieService.delete('korpaId');
    }
  }



}
