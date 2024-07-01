import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Racun } from 'src/app/models/ui-models/racun.model';
import { ViewService } from '../../services/view.service';

@Component({
  selector: 'app-racun-view',
  templateUrl: './racun-view.component.html',
  styleUrls: ['./racun-view.component.css']
})
export class RacunViewComponent implements OnInit{

  racun:Racun[]=[];

  displayedColumns: string[] = ['RacunId', 'UkupanIznos','DatumKupovine',  'VremeKupovine', 'Popust','ProcenatPop','IznosSaPopustom','Status','edit'];
  dataSource: MatTableDataSource<Racun>= new MatTableDataSource<Racun>();

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;



  constructor(private viewService: ViewService){}

  ngOnInit(): void {

    this.viewService.getRacuns()
    .subscribe(
      (successResponse)=>{
        this.racun=successResponse;
        this.dataSource=new MatTableDataSource<Racun>(this.racun);

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
}
}
