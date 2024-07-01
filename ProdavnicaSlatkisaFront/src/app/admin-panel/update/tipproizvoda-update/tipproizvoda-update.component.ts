import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ViewService } from '../../services/view.service';
import { TipProizvoda } from 'src/app/models/ui-models/tipproizvoda.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tipproizvoda-update',
  templateUrl: './tipproizvoda-update.component.html',
  styleUrls: ['./tipproizvoda-update.component.css']
})
export class TipproizvodaUpdateComponent implements OnInit {
  tipProizvodaId: number | undefined;
  tipproizvodum:TipProizvoda={
    tipProizvodaId:0,
	  sastav:''
  }

  constructor(private readonly viewService: ViewService, private readonly route: ActivatedRoute, private snackBar: MatSnackBar, private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      this.tipProizvodaId = idParam ? Number(idParam) : undefined;

      if (this.tipProizvodaId) {
        this.viewService.getTipProizvoda(this.tipProizvodaId)
        .subscribe(
          (successResponse)=>{
            this.tipproizvodum=successResponse;
          }
        );
      }
    });
  }

  OnUpdate():void{


    this.viewService.updateTipProizvoda(this.tipproizvodum.tipProizvodaId, this.tipproizvodum)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Tip proizvoda updated successfully!', undefined, {
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

    this.viewService.deleteTipProizvoda(this.tipproizvodum.tipProizvodaId)
    .subscribe(
      (successResponse)=>{
        this.snackBar.open('Tip deleted successfully!', undefined, {
          duration:2000
        });
        setTimeout(()=>{

          this.router.navigateByUrl('api/ModelView');

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
