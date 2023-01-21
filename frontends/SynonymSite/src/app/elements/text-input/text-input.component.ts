import {AfterViewInit, Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements AfterViewInit {
text:string="";
@Input() placeholder:string="";
@Output() onEnterPressed=new EventEmitter<void>();

  ngAfterViewInit(): void {
    var input=document.getElementById('input');
    if(input){
      input.focus();
    }
  }

  onEnter() {
    this.onEnterPressed.emit();
  }
}
