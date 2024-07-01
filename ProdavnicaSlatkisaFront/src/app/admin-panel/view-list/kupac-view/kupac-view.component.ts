import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Korisnik, Kupac } from 'src/app/models/ui-models/kupac.model';
import { ViewService } from '../../services/view.service';

@Component({
  selector: 'app-kupac-view',
  templateUrl: './kupac-view.component.html',
  styleUrls: ['./kupac-view.component.css']
})
export class KupacViewComponent implements OnInit{
  kupac:Kupac[]=[];
  korisnik:Korisnik[]=[];

  displayedColumns: string[] = ['usernameRk','lozinkaRk','brojKupovina'];
  dataSource: MatTableDataSource<Kupac>= new MatTableDataSource<Kupac>();

  constructor(private viewService: ViewService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;

  ngOnInit(): void {

    this.viewService.getKupacs()
    .subscribe(
      (successResponse)=>{
        this.kupac=successResponse;
        this.dataSource=new MatTableDataSource<Kupac>(this.kupac);

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
