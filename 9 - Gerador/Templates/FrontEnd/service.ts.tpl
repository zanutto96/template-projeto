import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { ApiService } from 'app/common/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class #Entity#Service {

  constructor(private api: ApiService) { } 

  get(resource: any, route: any, filters: any, showLoading: boolean = true): Observable<any> {

    return this.api.get(resource, route, filters, showLoading);
  }
   
  save(resource: any, model: any): Observable<any> {

    return this.api.post(resource, model);
  }

  savePartial(resource: any, model: any): Observable<any> {

    return this.api.put(resource, model);
  }

  delete(resource: any, model: any): Observable<any> {

    return this.api.delete(resource, model);
  }
}
