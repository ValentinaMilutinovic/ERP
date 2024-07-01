export class Administrator {
  adminID:number;
  username: string;
  lozinka: string;
  jmbg: string;
  korisnik: Korisnik;

  constructor() {
    this.adminID=0;
    this.username = '';
    this.lozinka = '';
    this.jmbg = '';

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
