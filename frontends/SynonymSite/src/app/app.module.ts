import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { TopBarComponent } from './elements/top-bar/top-bar.component';
import { SynonymBoxComponent } from './elements/synonym-box/synonym-box.component';
import { MainBoxComponent } from './elements/main-box/main-box.component';
import { InputSearchComponent } from './elements/input-search/input-search.component';
import { SynonymWordComponent } from './elements/synonym-word/synonym-word.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import { AddModelComponent } from './elements/add-model/add-model.component';
import { TextInputComponent } from './elements/text-input/text-input.component';
import {SearchService} from "./services/search.service";
import {SynonymsApiService} from "./services/api-services/synonyms-api.service";
import {CommonModule} from "@angular/common";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TopBarComponent,
    SynonymBoxComponent,
    MainBoxComponent,
    InputSearchComponent,
    SynonymWordComponent,
    AddModelComponent,
    TextInputComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),
  ],
  providers: [SearchService,SynonymsApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
