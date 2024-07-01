import { ProfileService } from './../profile.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginServiceService } from 'src/app/services/login-service.service';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';
import { Korpa } from 'src/app/models/ui-models/korpa.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  korpaRequest=new Korpa();
  private uloga='';

  constructor(
    private formBuilder: FormBuilder,
    private loginServiceService: LoginServiceService,
    private http: HttpClient,
    private router:Router,
    private profileService:ProfileService



    ) {
    this.loginForm = this.formBuilder.group({
      user: ['',[Validators.required, Validators.email, this.emailFormatValidator]],
      pass: ['', Validators.required]
    });
  }


  ngOnInit(): void {
    this.uloga=this.profileService.getUloga();
  }

  get f() { return this.loginForm.controls; }


  login() {
    if (this.loginForm.invalid) {
      console.error('Please provide a username and password');
      return;
    }

    const user = this.f['user'].value;
    const pass = this.f['pass'].value;

    const body = {
      usernameRk: user,
      lozinkaRk: pass,
    };

    this.http.post('https://localhost:44384/api/Kupac/login', body, { responseType: 'text' }).subscribe(
    token => {

      console.log(token);
      this.loginServiceService.setToken(token);
      this.loginServiceService.setUsername(user);
      if (this.uloga=="Admin") {
        this.router.navigateByUrl('/dashboard'); // Redirect to /Admin-panel
      } else {
        // Call createKorpa() after successful login
        this.createKorpa();
        this.router.navigateByUrl('/'); // Redirect to the home route
      }
    // Perform further actions with the token
    },
    error => {
     console.error(error);
    // Display an error message to the user
    }
  );
  }
  createKorpa(): void {
    // Get the korisnikId from the LoginService
    const kupacId = this.loginServiceService.getKupacId();


    this.korpaRequest.korisnikId = kupacId;

    console.log('User ID:', kupacId);

    try {
      // Call the API to create the "korpa"
      this.loginServiceService.createKorpa().subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("Korpa created:", response);

          this.loginServiceService.setKorpaId(response.korpaId);
          console.log(response.korpaId);
        },
        (error: any) => {
          // Handle error while creating "korpa"
          console.error("Error creating korpa:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "korpa"
      console.error("Error creating korpa:", error);
    }
  }
  getErrorMessage() {
    if (this.loginForm.get('user')?.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.loginForm.get('user')?.hasError('email')) {
      return 'Not a valid email';
    }

    return 'Email must contain "@" symbol and ".com" domain';
  }

  emailFormatValidator(control: FormControl) {
    const email = control.value;
    if (email && email.indexOf('@') === -1) {
      return { missingAtSymbol: true };
    }

    if (email && email.indexOf('.com') === -1) {
      return { missingDotCom: true };
    }

    return null;
  }

  markAllAsTouched() {
    Object.values(this.loginForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }
  logout() {
    this.loginServiceService.logout(); // Call the logout() method from the LoginServiceService
  }
}
