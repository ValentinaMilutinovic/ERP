import { Component, OnInit } from '@angular/core';
import { ProizvodService } from '../proizvod.service';
import { ActivatedRoute } from '@angular/router';
import { Proizvod } from 'src/app/models/ui-models/proizvod.model';
import { HttpClient } from '@angular/common/http';
import { LoginServiceService } from 'src/app/services/login-service.service';

@Component({
  selector: 'app-view-proizvod',
  templateUrl: './view-proizvod.component.html',
  styleUrls: ['./view-proizvod.component.css']
})
export class ViewProizvodComponent implements OnInit {
  proizvodId: number | null | undefined;
  proizvod: Proizvod | undefined;
  value: number = 1;
  activeTab: string = 'tipProizvoda';
  constructor(
    private readonly proizvodService: ProizvodService,
    private readonly route: ActivatedRoute,
    private readonly http: HttpClient,
    private readonly service:LoginServiceService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const idproizvoda = params.get('id');
      if (idproizvoda) {
        this.proizvodId = parseInt(idproizvoda, 10);
        this.proizvodService.getProizvod(this.proizvodId).subscribe(
          (successResponse) => {
            this.proizvod = successResponse;
            console.log(this.proizvod);
          },
          (errorResponse) => {
            console.log(errorResponse);
          }
        );
      }
    });
  }
  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }
  decreaseValue() {
    if (this.value > 1) {
      this.value--;
    }
  }

  increaseValue() {
    this.value++;
  }
  generateRandomInt(): number {
    return Math.floor(Math.random() * 100000) + 1; // Adjust the range as needed
  }
  addToCart() {
    const addProizvodUKorpiRequest = {
      proizUkorpiId: this.generateRandomInt(),
      brojKomada: this.value, // Get the value from the num button
      idproizvoda: this.proizvodId,
      korpaId: this.service.getKorpaId() // Use the existing korpaId
    };

    this.http.post('https://localhost:44384/api/ProizvodUKorpi', addProizvodUKorpiRequest)
      .subscribe(
        (cartItemResponse: any) => {
          // Handle success response for creating the cart item
          console.log(cartItemResponse);
          // Do any additional handling or redirect to the cart page
        },
        (cartItemError) => {
          // Handle error response for creating the cart item
          console.error(cartItemError);
        }
      );
  }
}
