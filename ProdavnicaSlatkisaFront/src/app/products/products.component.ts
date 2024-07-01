import { ParampService } from '../paramp.service';
import { Proizvod } from '../models/ui-models/proizvod.model';
import { Component, OnInit, OnDestroy,ViewChild } from '@angular/core';
import {ProizvodService} from './proizvod.service'


import { Subscription,interval  } from 'rxjs';
import { SearchService } from '../searches/search.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy {

  proizvodi:Proizvod[]=[];
  filteredProizvodi: Proizvod[] = [];
  searchText!: string;
  images = ["/assets/banner1.jpg", "/assets/banner2.jpg", "/assets/banner3.jpg"];
  activeImageIndex = 0;
  autoScrollInterval = 5000;
  autoScrollSubscription: Subscription | undefined;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  sortCriteria: string = '';

  constructor(private proizvodService: ProizvodService,private searchService: SearchService, private parampService:ParampService){}
  ngOnInit(): void {

    this.proizvodService.getProizvodi()

    .subscribe(

      (successResponse)=>{

        this.proizvodi=successResponse;
        this.filteredProizvodi = this.proizvodi;
        this.updatePagination();
      },
      (errorResponse)=>{
        console.log(errorResponse);
      }

    );
    this.startAutoScroll();

    this.searchService.searchText$.subscribe((searchText: string) => {
      this.searchText = searchText;
      this.filtriraniProizvodi();
    });

    this.parampService.tipProizvodaPramp$.subscribe((types: string[]) => {
      this.filterProizvodiByTip(types);
    });
  }

  ngAfterViewInit(): void {
    this.paginator.page.subscribe((event: PageEvent) => {
      this.updatePagination();
    });
  }

  onPageChange(event: PageEvent): void {
    this.paginator.pageSize = event.pageSize;
    this.paginator.pageIndex = event.pageIndex;
    this.updatePagination();
  }
  updatePagination(): void {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    const endIndex = startIndex + this.paginator.pageSize;
    this.filteredProizvodi = this.proizvodi.slice(startIndex, endIndex);
  }
  onSearchTextChanged(): void {
    this.searchService.setSearchText(this.searchText);
  }

  filtriraniProizvodi(): void {
    const searchTerm = this.searchText.toLowerCase().trim();

    this.filteredProizvodi = this.proizvodi.filter((proizvod: Proizvod) => {
      const proizvodjac =proizvod.idproizvodjacaNavigation ? proizvod.idproizvodjacaNavigation.nazivProizvodjaca.toLowerCase() : '';

      const tipNaziv = proizvod.tipProizvoda ? proizvod.tipProizvoda.sastav.toLowerCase() : '';

      return (
        proizvodjac.includes(searchTerm) ||
        tipNaziv.includes(searchTerm) ||
        proizvod.naziv.toLowerCase().includes(searchTerm)
      );
    });
  }
  filterProizvodiByTip(tip: string[]): void {
    if (tip && tip.length > 0) {
      this.filteredProizvodi = this.proizvodi.filter(pro =>
        tip.includes(pro.tipProizvoda?.sastav)
      );
    } else {
      this.filteredProizvodi = this.proizvodi;
    }

  }

  ngOnDestroy(): void {
    this.stopAutoScroll();
  }
  goToImage(index: number): void {
    this.activeImageIndex = index;
  }
  getCurrentImage(): string {
    return this.images[this.activeImageIndex];
  }

  nextImage(): void {
    this.activeImageIndex = (this.activeImageIndex + 1) % this.images.length;
  }

  prevImage(): void {
    this.activeImageIndex = (this.activeImageIndex - 1 + this.images.length) % this.images.length;
  }

  startAutoScroll(): void {
    this.stopAutoScroll(); // Stop any existing auto-scroll subscription

    this.autoScrollSubscription = interval(this.autoScrollInterval).subscribe(() => {
      this.nextImage();
    });
  }

  stopAutoScroll(): void {
    if (this.autoScrollSubscription) {
      this.autoScrollSubscription.unsubscribe();
    }
  }

  onSearch(): void {
    // Perform search logic using the searchText value
    const searchTerm = this.searchText.trim().toLowerCase();

    // Filter the products array based on the search term
    this.filteredProizvodi = this.proizvodi.filter((proizvod: Proizvod) => {
      // Modify the conditions based on your specific search requirements
      return (
        proizvod.idproizvodjacaNavigation.nazivProizvodjaca.toLowerCase().includes(searchTerm) ||
        proizvod.tipProizvoda.sastav.toLowerCase().includes(searchTerm)
      );
    });

    console.log('Search Text:', searchTerm);
  }
  sortCards(criteria: string): void {
    this.sortCriteria = criteria;

    if (criteria === 'low') {
      this.filteredProizvodi.sort((a, b) => a.cena - b.cena); // Sort by lowest cena
    } else if (criteria === 'high') {
      this.filteredProizvodi.sort((a, b) => b.cena - a.cena); // Sort by highest cena
    }
  }


}
