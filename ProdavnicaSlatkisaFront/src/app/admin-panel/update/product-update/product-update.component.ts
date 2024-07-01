import { Component, OnInit } from '@angular/core';
import { ViewService } from '../../services/view.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Proizvod } from 'src/app/models/ui-models/proizvod.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Proizvodjac } from 'src/app/models/ui-models/proizvodjac.model';
import { TipProizvoda } from 'src/app/models/ui-models/tipproizvoda.model';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.css']
})
export class ProductUpdateComponent implements OnInit {
  idproizvoda: number | undefined;
  proizvod:Proizvod={
    idproizvoda:0,
    cena:0,
    idproizvodjaca:0,
    idproizvodjacaNavigation:{

      idproizvodjaca:0,
      nazivProizvodjaca:'',
      zemljaPorekla:''

    },
    tipProizvodaId:0,
    tipProizvoda: {

      tipProizvodaId:0,
	    sastav:''

    },
    kolicinaNaStanju:0,
    naziv: ''


  }

  proizvodjacList:Proizvodjac[]=[];
  tipProizvodaList:TipProizvoda[]=[];

  constructor(private readonly viewService: ViewService, private readonly route: ActivatedRoute, private readonly snackBar:MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.idproizvoda = idParam ? Number(idParam) : undefined;

      if (this.idproizvoda) {
        this.viewService.getProizvod(this.idproizvoda)
        .subscribe(
          (successResponse)=>{
            this.proizvod=successResponse;
          }
        );
        this.viewService.getProizvodjacs()
        .subscribe(
          (successResponse)=>{
            this.proizvodjacList=successResponse;
          }
        );
        this.viewService.getaTipProizvods()
        .subscribe(
          (successResponse)=>{
            this.tipProizvodaList=successResponse;
          }
        );

      }
    });
  }

  onUpdate():void{


    this.viewService.updateProizvod(this.proizvod.idproizvoda, this.proizvod)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Model updated successfully!', undefined, {
          duration:2000
        });
      },
      (errorResponse)=>{
        this.snackBar.open('Update failed', undefined, {
          duration:2000
        });

      }
    );



  }

  onDelete():void{

    this.viewService.deleteProizvod(this.proizvod.idproizvoda)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Proizvod deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('api/ProizvodView');

        },2000)

      },
      (errorResponse)=>{

        this.snackBar.open('Delete failed', undefined, {
          duration:2000
        });
      }
    )


  }
}
