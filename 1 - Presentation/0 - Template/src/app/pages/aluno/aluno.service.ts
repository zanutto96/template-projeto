
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../../common/services/api.service';
import { Aluno } from './aluno.model';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  constructor(private api: ApiService) { }

  getAluno(filters?: Partial<Aluno>): Observable<{ data: { dataList: Aluno[] } }> {
    return this.api.get('Aluno', 'GetData', filters);
  }

  getAlunoById(filters?: Partial<Aluno>): Observable<Aluno> {
    return this.api.get('Aluno', 'GetById', filters);
  }

  saveAluno(model: Partial<Aluno>): Observable<Aluno> {
    return this.api.post('Aluno', model);
  }

  updateAluno(model: Partial<Aluno>): Observable<Aluno> {
    return this.api.put('Aluno', model);
  }

  deleteAluno(model: Partial<Aluno>): Observable<void> {
    return this.api.delete('Aluno', model);
  }
}
