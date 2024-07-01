import { TipProizvoda } from './tipproizvoda.model';
import { Proizvodjac } from './proizvodjac.model';

export interface Proizvod{
  idproizvoda:number,
	cena:number,
  idproizvodjaca:number,
  idproizvodjacaNavigation: Proizvodjac
  tipProizvodaId:number;
  tipProizvoda: TipProizvoda,
  kolicinaNaStanju:number,
  naziv: string

}
