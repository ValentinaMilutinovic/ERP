import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TipProizvoda } from 'src/app/models/ui-models/tipproizvoda.model';
import { ViewService } from '../../services/view.service';

@Component({
  selector: 'app-tipproizvoda-view',
  templateUrl: './tipproizvoda-view.component.html',
  styleUrls: ['./tipproizvoda-view.component.css']
})
export class TipproizvodaViewComponent implements OnInit{
  tipproizvoda:TipProizvoda[]=[];

  displayedColumns: string[] = ['sastav','edit'];
  dataSource: MatTableDataSource<TipProizvoda>= new MatTableDataSource<TipProizvoda>();

  constructor(private viewService: ViewService){}

@ViewChild(MatPaginator) matPaginator!:MatPaginator;
@ViewChild(MatSort) matSort!:MatSort;


  ngOnInit(): void {

    this.viewService.getaTipProizvods()
    .subscribe(
      (successResponse)=>{
        this.tipproizvoda=successResponse;
        this.dataSource=new MatTableDataSource<TipProizvoda>(this.tipproizvoda);

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
