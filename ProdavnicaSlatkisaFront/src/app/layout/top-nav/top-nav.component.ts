import { ParampService } from './../../paramp.service';
import { Component,OnInit } from '@angular/core';
import { SearchService } from 'src/app/searches/search.service';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent  {

  searchText!: string;


  constructor(private searchService: SearchService, private parampService:ParampService) { }

  ngOnInit(): void {
    this.searchService.searchText$.subscribe((searchText: string) => {
      // Handle the search text change
      this.searchText = searchText;
      console.log('Search Text:', this.searchText);
    });
  }

  onSearch(): void {
    // Emit the search text to the search service
    this.searchService.setSearchText(this.searchText);
  }


}
