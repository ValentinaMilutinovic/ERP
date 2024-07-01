import { Korpa } from "./korpa.model";

export interface AddBill {

  korpaId: number,
}

export interface Racun{
  racunId:number,
	ukupanIznos:number,
	datumKupovine:Date,
  vremeKupovine:string,
  popust:boolean,
  procenatPop:number,
  iznosSaPopustom:number,
  korpaId:number,
  korpa:Korpa,
  clientSecret:string,
  paymentIntentId:string,
  status:string

}
