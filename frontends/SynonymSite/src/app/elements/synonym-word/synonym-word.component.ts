import {AfterContentChecked, Component, Input} from '@angular/core';
import {SearchService} from "../../services/search.service";
import {SynonymsDto} from "../../models/SynonymsDto";

@Component({
  selector: 'app-synonym-word',
  templateUrl: './synonym-word.component.html',
  styleUrls: ['./synonym-word.component.scss']
})
export class SynonymWordComponent implements AfterContentChecked{
  @Input() synonym: SynonymsDto|undefined = undefined;
  levelClass: string="level0";
  constructor(private searchService: SearchService) {
  }



  onClick() {
    this.searchService.searchWord.next(this.synonym?.text!);
  }

  ngAfterContentChecked(): void {
    this.levelClass = "level" + (this.synonym?.level! > 3? 3:this.synonym?.level)
  }
}
