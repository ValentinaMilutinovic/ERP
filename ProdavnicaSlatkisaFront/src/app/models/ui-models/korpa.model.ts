export class Korpa{
  korisnikId:number;




  constructor( kupacId?: number)
  {
    this.korisnikId = kupacId !== undefined ? kupacId : 0;
  }
}
