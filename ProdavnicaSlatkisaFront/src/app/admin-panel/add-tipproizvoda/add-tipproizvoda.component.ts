import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

interface AddTipProizvodumRequest {
  sastav: string;
}

@Component({
  selector: 'app-add-tipproizvoda',
  templateUrl: './add-tipproizvoda.component.html',
  styleUrls: ['./add-tipproizvoda.component.css']
})
export class AddTipproizvodaComponent implements OnInit {
  addTipForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private http: HttpClient, private readonly snackBar:MatSnackBar, private router:Router) { }

  ngOnInit() {
    this.addTipForm = this.formBuilder.group({
      sastav: ['', Validators.required]
    });
  }

  addTip() {
    if (this.addTipForm.invalid) {
      return;
    }

    const addTipProizvodumRequest: AddTipProizvodumRequest = {
      sastav: this.addTipForm.value.sastav
    };

    this.http.post('https://localhost:44384/api/TipProizvodum', addTipProizvodumRequest)
      .subscribe(
        (response) => {
          this.snackBar.open('Tip added successfully!', undefined, {
            duration:2000
          });
          setTimeout(()=>{

            this.router.navigateByUrl('api/TipProizvodaView');

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
