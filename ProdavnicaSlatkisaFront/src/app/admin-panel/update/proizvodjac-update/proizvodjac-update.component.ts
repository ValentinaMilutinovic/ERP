import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewService } from '../../services/view.service';
import { Proizvodjac } from 'src/app/models/ui-models/proizvodjac.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-proizvodjac-update',
  templateUrl: './proizvodjac-update.component.html',
  styleUrls: ['./proizvodjac-update.component.css']
})
export class ProizvodjacUpdateComponent  implements OnInit {
  idproizvodjaca: number | undefined;
  proizvodjac:Proizvodjac={
    idproizvodjaca:0,
	  nazivProizvodjaca:'',
	  zemljaPorekla:''
  }

  constructor(private readonly viewService: ViewService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.idproizvodjaca = idParam ? Number(idParam) : undefined;

      if (this.idproizvodjaca) {
        this.viewService.getProizvodjac(this.idproizvodjaca)
        .subscribe(
          (successResponse)=>{
            this.proizvodjac= successResponse;
          }
        );
      }
    });
  }
  OnUpdate():void{


    this.viewService.updateProizvodjac(this.proizvodjac.idproizvodjaca, this.proizvodjac)
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

    this.viewService.deleteProizvodjac(this.proizvodjac.idproizvodjaca)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('proizvodjac deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('api/ProizvodjacView');

        },2000)

      },
      (errorResponse)=>{

        this.snackBar.open('Update failed', undefined, {
          duration:2000
        });
      }
    )


  }
}
