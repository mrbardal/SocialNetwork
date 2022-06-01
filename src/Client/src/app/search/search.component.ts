import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { SearchResult } from '../models/search-result.model';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  filter: FormControl;
  result: SearchResult;

  constructor(public userService: UserService) {
    this.filter = new FormControl('');
    this.result = new SearchResult([]);
  }

  ngOnInit(): void {
  }

  applyFilter() {
    this.userService.search(this.filter.value).subscribe(data => {
      // console.log(data);
      this.result = data;
    })
  }

  filterKeyDown(event: any) {
    this.applyFilter();
  }
}
