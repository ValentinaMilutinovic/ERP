import { Korpa } from "./korpa.model"
import { Proizvod } from "./proizvod.model"

export interface ProizvodUKorpi {
  proizUkorpiId:number,
  brojKomada: number,
  iznos: number,
  idproizvoda:number,
  proizvodNavigation:any,
  korpaId:number,
  korpa:Korpa
}
