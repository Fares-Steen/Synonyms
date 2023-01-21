import {Component, EventEmitter, Input, OnDestroy, Output} from '@angular/core';
import {SearchService} from "../../services/search.service";
import {Subscription} from "rxjs";
import {SynonymsDto} from "../../models/SynonymsDto";

@Component({
  selector: 'app-synonym-box',
  templateUrl: './synonym-box.component.html',
  styleUrls: ['./synonym-box.component.scss']
})
export class SynonymBoxComponent implements OnDestroy{
   subscription: Subscription;
  constructor(private searchService:SearchService) {
    this.subscription =searchService.synonyms.subscribe(value=> this.synonyms=value)
  }
  synonyms: SynonymsDto[]|undefined = undefined;
  @Output() onAdd = new EventEmitter<void>;

  onAddClicked(){
    this.onAdd.emit();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
