import { Proizvod } from 'src/app/models/ui-models/proizvod.model';
import { ViewService } from '../../services/view.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Proizvodjac } from 'src/app/models/ui-models/proizvodjac.model';
import { TipProizvoda } from 'src/app/models/ui-models/tipproizvoda.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-proizvod-view',
  templateUrl: './proizvod-view.component.html',
  styleUrls: ['./proizvod-view.component.css']
})
export class ProizvodViewComponent implements OnInit{

  proizvod:Proizvod[]=[];
  proizvodjac:Proizvodjac[]=[];
  tipp: TipProizvoda[]=[];
  displayedColumns: string[] = ['nazivProizvodjaca', 'naziv','sastav', 'cena','kolicinaNaStanju','edit'];
  dataSource: MatTableDataSource<Proizvod>= new MatTableDataSource<Proizvod>();

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;



  constructor(private viewService: ViewService){}

  ngOnInit(): void {

    this.viewService.getProizvodi()
    .subscribe(
      (successResponse)=>{
        this.proizvod=successResponse;
        this.dataSource=new MatTableDataSource<Proizvod>(this.proizvod);

        if(this.matPaginator){
          this.dataSource.paginator=this.matPaginator;
        }
        if(this.matSort){
          this.dataSource.sort=this.matSort;
        }
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )

    this.viewService.getProizvodjacs()
    .subscribe(
      (successResponse)=>{
        this.proizvodjac=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )
    this.viewService.getaTipProizvods()
    .subscribe(
      (successResponse)=>{
        this.tipp=successResponse
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }
    )



  }

}
