import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { ToastrService } from 'ngx-toastr';
import { Korpa } from 'src/app/models/ui-models/korpa.model';
import { CartsrService } from '../cartsr.service';
import { LoginServiceService } from 'src/app/services/login-service.service';
import { DialogComponent } from 'src/app/dialog/dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  @Input() paymentForm!: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  submitted = false;
  stripe : Stripe | null = null;
  cardNumber?:StripeCardNumberElement;
  cardExpiry?:StripeCardExpiryElement;
  cardCvc?:StripeCardCvcElement;
  cardErrors:any;
  clientSecret:string='';
  billId:number=0;
  errorMes?:string='';
  korpaId:number=0;
  korpaRequest=new Korpa();
  cardNumberComplete: boolean = false;
  cardExpiryComplete: boolean = false;
  cardCvcComplete: boolean = false;
  nameOnCardComplete:boolean=false;

  constructor(private formBuilder: FormBuilder, private cartsrService:CartsrService,private dialog: MatDialog,private toastr : ToastrService, private router:Router, private loginServiceService:LoginServiceService) {
    this.paymentForm = this.formBuilder.group({
      nameOnCard: ['', Validators.required],
      cardNumber: ['', [Validators.required, Validators.pattern('[0-9]{16}')]],
      cardExpiry: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$')]],
      cardCvc: ['', [Validators.required, Validators.pattern('^[0-9]{3}$')]],
    });
  }


  ngOnInit(): void {
  loadStripe('pk_test_51PXPJ2BWLa384iS0Ep9Jbmeq22baSLymkQputDw1OMdyX2kEKd0Naoiw1aa92tbWkQomS7c9rGDR5Smdwhrjz8G500rvm344PY').then(stripe=>{
      this.stripe=stripe;
      const elements = stripe?.elements();
      if (elements){
        this.cardNumber=elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change',event =>{
          this.cardNumberComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })

        this.cardExpiry=elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change',event =>{
          this.cardExpiryComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })

        this.cardCvc=elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change',event =>{
          this.cardCvcComplete=event.complete;
          if(event.error) this.cardErrors=event.error.message;
          else this.cardErrors=null;
        })
      }
    })
    this.billId=this.cartsrService.getRacunId();
  }

  get formControls() {
    return this.paymentForm.controls;
  }

  get paymentFormComplete(){
    if(!this.paymentForm.get('nameOnCard')?.hasError('required')){
      this.nameOnCardComplete=true;
    }
    return this.nameOnCardComplete
    && this.cardNumberComplete
    && this.cardExpiryComplete
    && this.cardCvcComplete
  }


  openDialog() {
    const dialogRef = this.dialog.open(DialogComponent, {
      width: '250px',
      data: 'Zahvaljujemo se, Vaša porudžbina je uspešno primeljena' // Pass the message to the dialog component
    });

    dialogRef.afterClosed().subscribe(result => {
      this.router.navigateByUrl('/')
      // Perform any additional actions after the dialog is closed (if needed)
    });
  }





  onSubmit(): void {
    this.submitted = true;

    this.cartsrService.getBillById(this.billId).subscribe(
      (bill) => {
        this.clientSecret = bill.clientSecret;

        this.stripe?.confirmCardPayment(this.clientSecret, {
          payment_method: {
            card: this.cardNumber!,
            billing_details: {
              name: this.paymentForm.get('nameOnCard')?.value
            }
          }
        }).then(result => {
          console.log(result);
          if (result.paymentIntent) {
            this.openDialog();
            /*this.korpaId = this.loginServiceService.generateRandomKorpaId();
            this.loginServiceService.setKorpaId(this.korpaId);*/
            this.createKorpa();
          } else {
            this.toastr.error(result.error.message);
          }
        })
      });
  }


  submitForm(): void {
    this.onSubmit();
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
  createRacun(): void {
    this.cartsrService.createRacun().subscribe(
      (response: any) => {
        console.log("Racun created:", response);
        // Perform any additional actions or handle the response as needed
      },
      (error: any) => {
        console.error("Error creating racun:", error);
        // Handle the error as needed
      }
    );
  }

}
