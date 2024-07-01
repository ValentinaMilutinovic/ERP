import { Component, OnInit } from '@angular/core';
import { CartsrService } from '../cartsr.service';

@Component({
  selector: 'app-shipping-info',
  templateUrl: './shipping-info.component.html',
  styleUrls: ['./shipping-info.component.css']
})
export class ShippingInfoComponent implements OnInit {
  constructor(private cartsService: CartsrService){}
  ngOnInit(): void {}


  createPaymentIntent() {

    const racunId=this.cartsService.getRacunId();

    try {
      // Call the API to create the "racun"
      this.cartsService.createPaymentIntent(racunId).subscribe(
        (response: any) => {
          // Log the response to the console
          console.log("PaymentIntent created:", response);
        },
        (error: any) => {
          // Handle error while creating "PaymentIntent"
          console.error("Error creating PaymentIntent:", error);
        }
      );
    } catch (error) {
      // Handle error while creating "PaymentIntent"
      console.error("Error creating PaymentIntent:", error);
    }
  }
}
