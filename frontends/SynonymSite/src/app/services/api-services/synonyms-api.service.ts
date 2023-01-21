import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {AddSynonymDto} from "../../models/AddSynonymDto";
import {ToastrService} from "ngx-toastr";
import {SynonymsDto} from "../../models/SynonymsDto";
@Injectable({
  providedIn: 'root'
})
export class SynonymsApiService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient,private toastr: ToastrService) { }

  public getSynonyms(word:string):Promise<SynonymsDto[]>{
    return new Promise<SynonymsDto[]>((resolve,reject)=>{
      this.http.get<SynonymsDto[]>(this.baseUrl+'/api/Synonym/getSynonyms?word='+word).subscribe({
        next: value => resolve(value),
        error: (e) => reject(e),
        complete: () => console.info('getSynonyms complete')
      });
    });
  }

  public addSynonym(addSynonymDto:AddSynonymDto):Promise<void>{
    return new Promise<void>((resolve, reject) => {
      this.http.post<void>(this.baseUrl+"/api/Synonym/AddSynonym",addSynonymDto).subscribe({
        error: (e) => {
          reject(e)
          this.toastr.error(typeof(e.error)=='string'?e.error:'unknown error',addSynonymDto.synonym+' Could not be added', {
            disableTimeOut: true,
          });
        },
        complete: () => {
          resolve();
          this.toastr.success(addSynonymDto.synonym, 'Successfully added');
        }
      })
    })
  }

  public ClearData() :Promise<void>{
    return new Promise<void>((resolve, reject) => {
      this.http.delete<void>(this.baseUrl+"/api/Synonym/RemoveAll").subscribe({
        error: (e) => {
          reject(e)
          this.toastr.error(typeof(e.error)=='string'?e.error:'unknown error',' Could not clear data', {
            disableTimeOut: true,
          });
        },
        complete: () => {
          resolve();
          this.toastr.success('Successfully clearedData');
        }
      })
    })
  }

}
