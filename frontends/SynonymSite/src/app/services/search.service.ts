import { Injectable } from '@angular/core';
import {BehaviorSubject, Subject} from "rxjs";
import {SynonymsApiService} from "./api-services/synonyms-api.service";
import {SynonymsDto} from "../models/SynonymsDto";

@Injectable({
  providedIn: 'root'
})
export class SearchService {
public searchWord:BehaviorSubject<string> = new BehaviorSubject<string>("");
public synonyms:BehaviorSubject<SynonymsDto[]|undefined> = new BehaviorSubject<SynonymsDto[]|undefined>(undefined);
  constructor(private synonymsApiService:SynonymsApiService) {
  this.searchWord.subscribe(async (value)=>{
    if(value=="")return;
   this.synonyms.next(await synonymsApiService.getSynonyms(value));
  });
  }
}
