import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

export interface Aluno {
  id?: number;
  nomeCompleto?: string;
  dataNascimento?: Date;
  cpf?: string;
  email?: string;
  telCelular?: string;
  endereco?: string;
  bairro?: string;
  cidade?: string;
  estado?: string;
  cep?: string;
  usuarioId?: number;
}

export interface AlunoFilter {
  id?: number;
  nomeCompleto?: string;
  cpf?: string;
  email?: string;
  pageIndex?: number;
  pageSize?: number;
}

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  constructor(private apiService: ApiService) { }

  getAlunos(filters?: AlunoFilter): Observable<any> {
    return this.apiService.get('Aluno', 'GetData', filters);
  }

  getAlunoById(id: number): Observable<any> {
    return this.apiService.get('Aluno', 'GetById', { id });
  }

  salvarAluno(aluno: Aluno): Observable<any> {
    return this.apiService.post('Aluno', aluno);
  }

  atualizarAluno(aluno: Aluno): Observable<any> {
    return this.apiService.put('Aluno', aluno);
  }

  excluirAluno(aluno: Aluno): Observable<any> {
    return this.apiService.delete('Aluno', aluno);
  }
}