import { Component, OnInit } from '@angular/core';
import { Proizvod } from '../models/ui-models/proizvod.model';
import { ProizvodService } from '../products/proizvod.service';
import { ProizvodUKorpi } from '../models/api-models/proizvodukorpi';
import { Korpa } from '../models/api-models/korpa.model';
import { LoginServiceService } from '../services/login-service.service';
import { CartsrService } from './cartsr.service';
import { switchMap } from 'rxjs';
import {UpdateProiUKo} from '../models/ui-models/update.proizvoduk.model'

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  proizvod: Proizvod[] | undefined;
  showDataNotFound = true;
  iznos:number|undefined;
  productsInCart: ProizvodUKorpi[] = []; // Array to store products in the cart
  korpa: Korpa | undefined;
  korpaId = this.loginServiceService.getKorpaId();
  // Not Found Message
  messageTitle = "No Products Found in Cart";
  messageDescription = "Please, Add Products to Cart";

  constructor(private proizvodService: ProizvodService, private loginServiceService: LoginServiceService,private cartsService: CartsrService) {}

  ngOnInit(): void {
    this.cartsService.getProductInShoppingCartByKorpaId(this.korpaId).subscribe(
      (successResponse) => {
        this.productsInCart = successResponse;

      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
    this.getShoppingCart();
  }

  getShoppingCart(): void {
    const korpaId = this.loginServiceService.getKorpaId();

    this.cartsService.getKorpaById(korpaId)
      .subscribe(
        (cartResponse) => {
          this.korpa = cartResponse;
          this.iznos=cartResponse.ukupanIznos;
        },


        (errorResponse) => {
          console.error('Error retrieving shopping cart:', errorResponse);
        }
      );
  }

  setIznos(): number {
    return this.iznos || 0; //  default value in case iznos is undefined
  }

  incrementQuantity(proizvodUk: ProizvodUKorpi): void {
    if (proizvodUk.brojKomada < proizvodUk.proizvodNavigation.kolicinaNaStanju) {
      proizvodUk.brojKomada++;
      this.updateQuantityInDatabase(proizvodUk);
    }
  }

  decrementQuantity(proizvodUk: ProizvodUKorpi): void {
    if (proizvodUk.brojKomada > 1) {
      proizvodUk.brojKomada--;
      this.updateQuantityInDatabase(proizvodUk);
    }
  }

  updateQuantityInDatabase(proizvodUk: ProizvodUKorpi): void {
    const proizvodUKId = proizvodUk.proizUkorpiId;
    const updateProiUKo = {
      brojKomada: proizvodUk.brojKomada,

    };

    this.cartsService.updateProductQuantity(updateProiUKo , proizvodUKId)
      .pipe(
        switchMap(() => this.cartsService.getKorpaById(this.korpaId))
      )
      .subscribe(
        (cartResponse) => {
          this.korpa = cartResponse;
          this.iznos = cartResponse.ukupanIznos;
        },
        (errorResponse) => {
          console.error('Error updating product quantity:', errorResponse);
        }
      );
  }


  getCartProduct() {
    const activeUserId = this.getActiveUserId(); // Call a method to get the active user ID
    if (activeUserId) {
      this.proizvodService.getCartProducts(activeUserId).subscribe(
        (products: Proizvod[]) => {
          if (products.length > 0) {
            this.proizvod = products;
            this.showDataNotFound = false;
          } else {
            this.showDataNotFound = true;
          }
        },
        (error: any) => {
          console.error('Error retrieving cart products:', error);
        }
      );
    }
  }
  getActiveUserId(): string | null {
    // Implement your logic to retrieve the active user ID
    // This can be from a service, local storage, or any other method
    // Return the active user ID or null if not available
    // Example implementation using local storage:
    const activeUserId = localStorage.getItem('activeUserId');
    return activeUserId ? activeUserId : null;
  }
  removeProduct(proizvodUk: ProizvodUKorpi): void {
    const proizvodUKId = proizvodUk.proizUkorpiId;

    this.cartsService.deleteProduct(proizvodUKId).subscribe(
      (successResponse) => {
        // Product successfully deleted from the cart
        // Perform any additional operations or UI updates if needed
        console.log('Product removed:', successResponse);
        // Remove the product from the local array
        this.productsInCart = this.productsInCart.filter(p => p.proizUkorpiId !== proizvodUKId);
        this.getShoppingCart();
      },
      (errorResponse) => {
        console.error('Error removing product:', errorResponse);
      }
    );
  }
  addBill() {
    try {
      // Call the API to create the "racun"
      this.cartsService.postBill(this.korpaId).subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("Racun created:", response);
        },
        (error: any) => {
          // Handle error while creating "racun"
          console.error("Error creating racun:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "racun"
      console.error("Error creating racun:", error);
    }
  }

 

}
