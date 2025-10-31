
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../common/services/api.service';
import { #Entity# } from './#entity#.model';

@Injectable({
  providedIn: 'root'
})
export class #Entity#Service {

  constructor(private api: ApiService) { }

  get#Entity#(filters?: Partial<#Entity#>): Observable<{ data: { dataList: #Entity#[] } }> {
    return this.api.get('#Entity#', 'GetData', filters);
  }

  get#Entity#ById(filters?: Partial<#Entity#>): Observable<#Entity#> {
    return this.api.get('#Entity#', 'GetById', filters);
  }

  save#Entity#(model: Partial<#Entity#>): Observable<#Entity#> {
    return this.api.post('#Entity#', model);
  }

  update#Entity#(model: Partial<#Entity#>): Observable<#Entity#> {
    return this.api.put('#Entity#', model);
  }

  delete#Entity#(model: Partial<#Entity#>): Observable<void> {
    return this.api.delete('#Entity#', model);
  }
}
