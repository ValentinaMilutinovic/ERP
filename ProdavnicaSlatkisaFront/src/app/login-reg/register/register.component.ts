import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private readonly snackBar:MatSnackBar,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      usernameRk: ['', [Validators.required, Validators.email, this.emailFormatValidator]],
      lozinkaRk: ['', Validators.required],
      ime: ['', Validators.required],
      prezime: ['', Validators.required],
      kontakt: ['', Validators.required],
      grad: ['', Validators.required],
      adresa: ['', Validators.required]
    });
  }

  get f() {
    return this.registerForm.controls;
  }
  getErrorMessage() {
    if (this.registerForm.get('usernameRk')?.hasError('required')) {
      return 'You must enter a value';
    }

    if (this.registerForm.get('usernameRk')?.hasError('email')) {
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

  register() {
    if (this.registerForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const usernameRk = this.f['usernameRk'].value;
    const lozinkaRk = this.f['lozinkaRk'].value;
    const ime = this.f['ime'].value;
    const prezime = this.f['prezime'].value;
    const kontakt = this.f['kontakt'].value;
    const grad = this.f['grad'].value;
    const adresa = this.f['adresa'].value;
    const email= this.f['usernameRk'].value;


    const body = {
      usernameRk: usernameRk,
      lozinkaRk: lozinkaRk,
      registrovan: true,
      kupac: {
        ime: ime,
        prezime: prezime,
        email: usernameRk, // Add email field if necessary
        kontakt: kontakt,
        grad: grad,
        adresa: adresa
      }
    };




    this.http
      .post('https://localhost:44384/api/Kupac/register', body, {
        responseType: 'text'
      })
      .subscribe(
        response => {
          console.log(response);
          this.snackBar.open('Registration successful!', undefined, {
            duration:2000
          });
          this.router.navigateByUrl('/api/Kupac/login');
        },
        error => {
          console.error(error);
          this.snackBar.open('Registration failed.', undefined, {
            duration:2000
          });
        }
      );

  }

  redirectToLogin() {
    // Implement redirection to the login component
  }
  markAllAsTouched() {
    Object.values(this.registerForm.controls).forEach(control => {
      control.markAsTouched();
    });
  }

}
