import { Component } from '@angular/core';
import { SearchService } from './search.service';

@Component({
  selector: 'app-searches',
  templateUrl: './searches.component.html',
  styleUrls: ['./searches.component.css']
})
export class SearchesComponent {
  constructor(private searchService: SearchService) { }

  onSearch(searchText: string): void {
    // Perform search logic using the searchText value
    console.log('Search Text:', searchText);

    // Pass the search text to the shared service
    this.searchService.setSearchText(searchText);
  }
}
