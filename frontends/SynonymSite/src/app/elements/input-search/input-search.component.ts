import {Component, OnDestroy} from '@angular/core';
import {SearchService} from "../../services/search.service";
import {SynonymsApiService} from "../../services/api-services/synonyms-api.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-input-search',
  templateUrl: './input-search.component.html',
  styleUrls: ['./input-search.component.scss']
})
export class InputSearchComponent implements OnDestroy{
  text:string="";
  subscription: Subscription;
  constructor(private searchService: SearchService, private synonymsApiService:SynonymsApiService) {
    this.subscription= searchService.searchWord.subscribe(value=> this.text=value);
  }

  onSearch() {
    this.searchService.searchWord.next(this.text);
  }

  async onClearData() {
    await this.synonymsApiService.ClearData();
    this.searchService.searchWord.next("");
    this.searchService.synonyms.next(undefined);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
