import {Component, OnDestroy, OnInit} from '@angular/core';
import {SynonymsApiService} from "../../services/api-services/synonyms-api.service";
import {SearchService} from "../../services/search.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-main-box',
  templateUrl: './main-box.component.html',
  styleUrls: ['./main-box.component.scss']
})
export class MainBoxComponent implements OnDestroy{
  synonyms:string[]=[];
  selectedWord=""
  showAddSynonymModel=false;
   subscription: Subscription;
  constructor(private searchService:SearchService,private synonymsApiService: SynonymsApiService) {
    this.subscription = this.searchService.searchWord.subscribe(v=>this.selectedWord=v);
  }

   onShowAddSynonymModel(){
    this.showAddSynonymModel=true;
  }
  onHideAddSynonymModel(){
    this.showAddSynonymModel=false;
  }
  async onAddSynonym(synonym:string){
    this.onHideAddSynonymModel();
    let word=this.searchService.searchWord.value;
    const addSynonymDto = {synonym:synonym,word:word}
    await this.synonymsApiService.addSynonym(addSynonymDto);
    this.searchService.searchWord.next(this.searchService.searchWord.value);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
