export class Kupac {
  kupacId:number;
  usernameRk: string;
  lozinkaRk: string;
  brojKupovina: string;
  registrovan: string;
  username: string;
  lozinka: string;
  korisnik: Korisnik;

  constructor() {
    this.kupacId=0;
    this.usernameRk = '';
    this.lozinkaRk = '';
    this.brojKupovina = '';
    this.registrovan = '';
    this.username = '';
    this.lozinka = '';
    this.korisnik = new Korisnik();
  }
}

export class Korisnik {
  ime: number;
  prezime: string;
  email: string;
  kontakt: string;
  adresa: string;
  grad: string;

  constructor() {
    this.ime = 0;
    this.prezime = '';
    this.email = '';
    this.kontakt = '';
    this.adresa = '';
    this.grad = '';
  }
}
