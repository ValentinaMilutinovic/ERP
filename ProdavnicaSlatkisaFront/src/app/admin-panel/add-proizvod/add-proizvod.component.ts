import { ViewService } from '../services/view.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AdminProductService } from '../admin-product.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

interface AddProizvodRequest {
	cena:number;
  idproizvodjaca:number;
  tipProizvodaId:number;
  kolicinaNaStanju:number;
  naziv: string;
}
interface Proizvodjac {
  idproizvodjaca: number;
  nazivProizvodjaca: string;
  zemljaPorekla: string;
}
interface TipProizvoda {
  tipProizvodaId:number,
  sastav: string;
}
@Component({
  selector: 'app-add-proizvod',
  templateUrl: './add-proizvod.component.html',
  styleUrls: ['./add-proizvod.component.css'],
   providers: [AdminProductService]
})
export class AddProizvodComponent implements OnInit {
  addProizvodForm!: FormGroup;
  proizvodjaci: Proizvodjac[] = [];
  tipoviProizvoda: TipProizvoda[] = [];

  constructor(private formBuilder: FormBuilder, private viewService: ViewService,private http: HttpClient, private readonly snackBar:MatSnackBar, private router:Router) { }

  ngOnInit() {
    this.addProizvodForm = this.formBuilder.group({
      cena: ['', Validators.required],
      idproizvodjaca: ['', Validators.required],
      tipProizvodaId: ['', Validators.required],
      kolicinaNaStanju: ['', Validators.required],
      naziv: ['', Validators.required],
    });
    this.fetchProizvodjaci();
    this.fetchTipoviProizvoda();
  }
  get f() {
    return this.addProizvodForm.controls;
  }
  fetchProizvodjaci() {
    // Simulated API call to fetch proizvodjaci data
    this.viewService.getProizvodjacs()
      .subscribe(
        (response) => {
          this.proizvodjaci = response;
        },
        (error) => {
          console.error('Failed to fetch proizvodjaci', error);
        }
      );
  }

  fetchTipoviProizvoda() {
    // Simulated API call to fetch tipoviProizvoda data
    this.viewService.getaTipProizvods()
      .subscribe(
        (response) => {
          this.tipoviProizvoda = response;
        },
        (error) => {
          console.error('Failed to fetch tipoviProizvoda', error);
        }
      );
  }
  addProizvod() {
    if (this.addProizvodForm.invalid) {
      console.error('Please fill in all the required fields');
      return;
    }

    const addProizvodRequest: AddProizvodRequest = {
      cena: this.addProizvodForm.value.cena,
      idproizvodjaca: this.addProizvodForm.value.idproizvodjaca,
      tipProizvodaId: this.addProizvodForm.value.tipProizvodaId,
      kolicinaNaStanju: this.addProizvodForm.value.kolicinaNaStanju,
      naziv: this.addProizvodForm.value.naziv
    };
    this.http
      .post('https://localhost:44384/api/Proizvod', addProizvodRequest)
      .subscribe(
        response => {
           this.snackBar.open('Product added successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('api/ProizvodView');

        },2000)
        },
        error => {

        this.snackBar.open('Adding failed', undefined, {
          duration:2000
        });
        }
      );

  }
}
