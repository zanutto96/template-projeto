
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../common/services/api.service';
import { AlunoStatus } from './alunostatus.model';

@Injectable({
  providedIn: 'root'
})
export class AlunoStatusService {

  constructor(private api: ApiService) { }

  getAlunoStatus(filters?: Partial<AlunoStatus>): Observable<{ data: { dataList: AlunoStatus[] } }> {
    return this.api.get('AlunoStatus', 'GetData', filters);
  }

  getAlunoStatusById(filters?: Partial<AlunoStatus>): Observable<AlunoStatus> {
    return this.api.get('AlunoStatus', 'GetById', filters);
  }

  saveAlunoStatus(model: Partial<AlunoStatus>): Observable<AlunoStatus> {
    return this.api.post('AlunoStatus', model);
  }

  updateAlunoStatus(model: Partial<AlunoStatus>): Observable<AlunoStatus> {
    return this.api.put('AlunoStatus', model);
  }

  deleteAlunoStatus(model: Partial<AlunoStatus>): Observable<void> {
    return this.api.delete('AlunoStatus', model);
  }
}
