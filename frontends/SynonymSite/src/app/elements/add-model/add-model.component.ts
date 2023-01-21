import { Component, EventEmitter, Output, ViewChild} from '@angular/core';
import {SearchService} from "../../services/search.service";
import {TextInputComponent} from "../text-input/text-input.component";

@Component({
  selector: 'app-add-model',
  templateUrl: './add-model.component.html',
  styleUrls: ['./add-model.component.scss']
})
export class AddModelComponent {
  text = ""
  word = ""
  @Output() onAdd=new EventEmitter<string>();
  @Output() onCancel=new EventEmitter<void>();
  @ViewChild(TextInputComponent) textInputComponent: TextInputComponent|undefined;
  constructor(private searchService:SearchService) {
    this.word = this.searchService.searchWord.value
  }

  onAddClicked() {
  this.onAdd.emit(this.textInputComponent?.text);
  }

  onCancelClicked() {
  this.onCancel.emit();
  }

}
