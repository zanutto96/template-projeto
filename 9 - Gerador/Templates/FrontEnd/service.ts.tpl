import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiService } from '../common/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class #Entity#Service {

  constructor(private api: ApiService) { }

  get#Entity#(filters?: any): Observable<any> {
    return this.api.get('#Entity#', 'GetData', filters);
  }

  get#Entity#ById(id: number): Observable<any> {
    return this.api.get('#Entity#', 'GetById', { id });
  }

  save#Entity#(model: any): Observable<any> {
    return this.api.post('#Entity#', model);
  }

  update#Entity#(model: any): Observable<any> {
    return this.api.put('#Entity#', model);
  }

  delete#Entity#(model: any): Observable<any> {
    return this.api.delete('#Entity#', model);
  }
}
