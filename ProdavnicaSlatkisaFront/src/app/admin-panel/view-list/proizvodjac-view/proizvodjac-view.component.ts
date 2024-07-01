import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Proizvodjac } from 'src/app/models/ui-models/proizvodjac.model';
import { ViewService } from '../../services/view.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-proizvodjac-view',
  templateUrl: './proizvodjac-view.component.html',
  styleUrls: ['./proizvodjac-view.component.css']
})
export class ProizvodjacViewComponent implements OnInit{
  proizvodjac:Proizvodjac[]=[];

  displayedColumns: string[] = ['nazivProizvodjaca','zemljaPorekla','edit'];
  dataSource: MatTableDataSource<Proizvodjac>= new MatTableDataSource<Proizvodjac>();

  constructor(private viewService: ViewService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;


  ngOnInit(): void {

    this.viewService.getProizvodjacs()
    .subscribe(
      (successResponse)=>{
        this.proizvodjac=successResponse;
        this.dataSource=new MatTableDataSource<Proizvodjac>(this.proizvodjac);

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
