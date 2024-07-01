import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


interface AddProizvodjacRequest {
  nazivProizvodjaca: string;
  zemljaPorekla: string;
}


@Component({
  selector: 'app-add-brand',
  templateUrl: './add-brand.component.html',
  styleUrls: ['./add-brand.component.css']
})
export class AddBrandComponent implements OnInit {
  addBrandForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private readonly snackBar:MatSnackBar, private router:Router) { }

  ngOnInit() {
    this.addBrandForm = this.formBuilder.group({
      nazivProizvodjaca: ['', Validators.required],
      zemljaPorekla: ['', Validators.required]
    });
  }

  addProizvodjac() {
    if (this.addBrandForm.invalid) {
      return;
    }

    const addProizvodjacRequest: AddProizvodjacRequest = {
      nazivProizvodjaca: this.addBrandForm.value.nazivProizvodjaca,
      zemljaPorekla: this.addBrandForm.value.zemljaPorekla
    };

    this.http.post('https://localhost:44384/api/Proizvodjac', addProizvodjacRequest)
      .subscribe(
        (response) => {
          this.snackBar.open('Brand added successfully!', undefined, {
            duration:2000
          });
          setTimeout(()=>{

            this.router.navigateByUrl('api/ProizvodjacView');

          },2000)
        },
        (error) => {
          this.snackBar.open('Adding failed', undefined, {
            duration:2000
          });
        }
      );
  }
}
