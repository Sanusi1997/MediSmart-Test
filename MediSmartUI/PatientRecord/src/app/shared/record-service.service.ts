import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { Record } from '../models/record.model';

@Injectable({
  providedIn: 'root'
})
export class RecordServiceService {

 
  readonly APIUrl = "https://localhost:44335/api/records"
  constructor(private http: HttpClient) { }

  formData : Record = new Record(); 

  postRecord(){
      return this.http.post(this.APIUrl, this.formData)
  }
  updateRecord(){
    return this.http.put(`${this.APIUrl}/${this.formData.patientId}`, this.formData)
}

  getRecord(): Observable<Record[]>{
    return this.http.get<Record[]>(this.APIUrl)
    }
   
    deleteRecord(patientId: number){
      return this.http.delete(`${this.APIUrl}/${patientId}`)
  }

}
