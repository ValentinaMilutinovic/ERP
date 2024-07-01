import { Korisnik } from "./kupac.model"

export interface Korpa {
  korpaId:number,
  korisnikId:number,
  ukupanIznos: number,
  brProizvoda: number
}
export interface ShowKorpa {
  korpaId:number,
  ukupanIznos: number,
  brProizvoda: number,
  korisnikId:number,
  korisnik:Korisnik
}
