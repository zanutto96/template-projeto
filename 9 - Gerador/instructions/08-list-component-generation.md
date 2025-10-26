# List Component Generation Instructions

## 🎯 Objetivo
Gerar componentes Angular de listagem baseado na estrutura de tabela fornecida.

## 📝 Template de Entrada
```
Table: [NomeTabela]
Entity: [NomeEntidade]
Display Columns: [Colunas para exibir]
Searchable Columns: [Colunas para busca]
```

## 🏗️ Estrutura do Componente de Lista

### Component TypeScript
```typescript
import { Component, OnInit } from '@angular/core';
import { [NomeEntidade] } from '../models/[nome-entidade].model';
import { [NomeEntidade]Service } from '../services/[nome-entidade].service';
import { [NomeEntidade]Filter } from '../models/[nome-entidade]-filter.model';

@Component({
  selector: 'app-[nome-entidade]-list',
  templateUrl: './[nome-entidade]-list.component.html',
  styleUrls: ['./[nome-entidade]-list.component.css']
})
export class [NomeEntidade]ListComponent implements OnInit {
  [nome-entidade]s: [NomeEntidade][] = [];
  loading = false;
  error = '';
  
  // Filtros
  filter: [NomeEntidade]Filter = {};
  
  // Paginação
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  totalPages = 0;

  constructor(private [nome-entidade]Service: [NomeEntidade]Service) { }

  ngOnInit(): void {
    this.load[NomeEntidade]s();
  }

  async load[NomeEntidade]s(): Promise<void> {
    try {
      this.loading = true;
      this.error = '';
      
      const data = await this.[nome-entidade]Service.getByFilter(this.filter);
      this.[nome-entidade]s = data;
      
      const count = await this.[nome-entidade]Service.count(this.filter);
      this.totalItems = count;
      this.updatePagination();
      
    } catch (error: any) {
      this.error = error.message || 'Erro ao carregar dados';
    } finally {
      this.loading = false;
    }
  }

  async delete[NomeEntidade](id: [TipoPK]): Promise<void> {
    if (!confirm('Deseja realmente excluir este registro?')) {
      return;
    }

    try {
      this.loading = true;
      await this.[nome-entidade]Service.delete(id);
      await this.load[NomeEntidade]s();
    } catch (error: any) {
      this.error = error.message || 'Erro ao excluir registro';
    } finally {
      this.loading = false;
    }
  }

  onSearch(): void {
    this.currentPage = 1;
    this.load[NomeEntidade]s();
  }

  clearFilter(): void {
    this.filter = {};
    this.currentPage = 1;
    this.load[NomeEntidade]s();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.load[NomeEntidade]s();
  }

  private updatePagination(): void {
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
  }
}
```

### Component HTML
```html
<div class="container-fluid">
  <div class="row">
    <div class="col-12">
      <div class="card">
        <div class="card-header d-flex justify-content-between align-items-center">
          <h4 class="card-title mb-0">Lista de [NomeEntidade]s</h4>
          <button class="btn btn-primary" routerLink="/[nome-entidade]/novo">
            <i class="fas fa-plus"></i> Novo
          </button>
        </div>
        
        <div class="card-body">
          <!-- Filtros -->
          <div class="row mb-3">
            <div class="col-12">
              <form class="row g-3">
                <!-- Campos de filtro baseados na estrutura da tabela -->
                <div class="col-md-3" *ngIf="hasStringColumn">
                  <label for="filterNome" class="form-label">Nome</label>
                  <input 
                    type="text" 
                    class="form-control" 
                    id="filterNome"
                    [(ngModel)]="filter.nome"
                    name="nome"
                    placeholder="Digite o nome...">
                </div>
                
                <div class="col-md-3" *ngIf="hasDateColumn">
                  <label for="filterDataInicio" class="form-label">Data Início</label>
                  <input 
                    type="date" 
                    class="form-control" 
                    id="filterDataInicio"
                    [(ngModel)]="filter.dataInicio"
                    name="dataInicio">
                </div>
                
                <div class="col-md-3" *ngIf="hasDateColumn">
                  <label for="filterDataFim" class="form-label">Data Fim</label>
                  <input 
                    type="date" 
                    class="form-control" 
                    id="filterDataFim"
                    [(ngModel)]="filter.dataFim"
                    name="dataFim">
                </div>
                
                <div class="col-md-3" *ngIf="hasBooleanColumn">
                  <label for="filterAtivo" class="form-label">Status</label>
                  <select 
                    class="form-control" 
                    id="filterAtivo"
                    [(ngModel)]="filter.ativo"
                    name="ativo">
                    <option value="">Todos</option>
                    <option [value]="true">Ativo</option>
                    <option [value]="false">Inativo</option>
                  </select>
                </div>
                
                <div class="col-12">
                  <button type="button" class="btn btn-outline-primary me-2" (click)="onSearch()">
                    <i class="fas fa-search"></i> Buscar
                  </button>
                  <button type="button" class="btn btn-outline-secondary" (click)="clearFilter()">
                    <i class="fas fa-times"></i> Limpar
                  </button>
                </div>
              </form>
            </div>
          </div>

          <!-- Loading -->
          <div *ngIf="loading" class="text-center py-3">
            <div class="spinner-border" role="status">
              <span class="visually-hidden">Carregando...</span>
            </div>
          </div>

          <!-- Error -->
          <div *ngIf="error" class="alert alert-danger" role="alert">
            {{ error }}
          </div>

          <!-- Tabela -->
          <div *ngIf="!loading && !error" class="table-responsive">
            <table class="table table-striped table-hover">
              <thead class="table-dark">
                <tr>
                  <!-- Cabeçalhos baseados na estrutura da tabela -->
                  <th>ID</th>
                  <th *ngFor="let column of displayColumns">{{ column.label }}</th>
                  <th width="150">Ações</th>
                </tr>
              </thead>
              <tbody>
                <tr *ngFor="let item of [nome-entidade]s">
                  <td>{{ item.id }}</td>
                  <!-- Células baseadas na estrutura da tabela -->
                  <td *ngFor="let column of displayColumns">
                    <span [ngSwitch]="column.type">
                      <span *ngSwitchCase="'date'">{{ item[column.field] | date:'dd/MM/yyyy' }}</span>
                      <span *ngSwitchCase="'datetime'">{{ item[column.field] | date:'dd/MM/yyyy HH:mm' }}</span>
                      <span *ngSwitchCase="'boolean'">
                        <span class="badge" 
                              [class.bg-success]="item[column.field]" 
                              [class.bg-danger]="!item[column.field]">
                          {{ item[column.field] ? 'Sim' : 'Não' }}
                        </span>
                      </span>
                      <span *ngSwitchDefault>{{ item[column.field] }}</span>
                    </span>
                  </td>
                  <td>
                    <button 
                      class="btn btn-sm btn-outline-primary me-1" 
                      [routerLink]="['/[nome-entidade]', item.id]"
                      title="Editar">
                      <i class="fas fa-edit"></i>
                    </button>
                    <button 
                      class="btn btn-sm btn-outline-danger" 
                      (click)="delete[NomeEntidade](item.id)"
                      title="Excluir">
                      <i class="fas fa-trash"></i>
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
            
            <!-- Mensagem se vazio -->
            <div *ngIf="[nome-entidade]s.length === 0" class="text-center py-3">
              <p class="text-muted">Nenhum registro encontrado.</p>
            </div>
          </div>

          <!-- Paginação -->
          <nav *ngIf="totalPages > 1" aria-label="Paginação">
            <ul class="pagination justify-content-center">
              <li class="page-item" [class.disabled]="currentPage === 1">
                <button class="page-link" (click)="onPageChange(currentPage - 1)">Anterior</button>
              </li>
              <li *ngFor="let page of [].constructor(totalPages); let i = index" 
                  class="page-item" 
                  [class.active]="currentPage === i + 1">
                <button class="page-link" (click)="onPageChange(i + 1)">{{ i + 1 }}</button>
              </li>
              <li class="page-item" [class.disabled]="currentPage === totalPages">
                <button class="page-link" (click)="onPageChange(currentPage + 1)">Próximo</button>
              </li>
            </ul>
          </nav>
          
          <!-- Info da paginação -->
          <div class="d-flex justify-content-between align-items-center mt-3">
            <small class="text-muted">
              Mostrando {{ ([nome-entidade]s.length === 0) ? 0 : ((currentPage - 1) * pageSize + 1) }} 
              até {{ Math.min(currentPage * pageSize, totalItems) }} 
              de {{ totalItems }} registros
            </small>
            <small class="text-muted">
              Página {{ currentPage }} de {{ totalPages }}
            </small>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
```

### Component CSS
```css
.card {
  border: none;
  box-shadow: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075);
  border-radius: 0.375rem;
}

.card-header {
  background-color: #f8f9fa;
  border-bottom: 1px solid #dee2e6;
  padding: 1rem 1.25rem;
}

.table th {
  border-top: none;
  font-weight: 600;
  font-size: 0.875rem;
}

.table td {
  vertical-align: middle;
  font-size: 0.875rem;
}

.btn-sm {
  padding: 0.25rem 0.5rem;
  font-size: 0.75rem;
}

.spinner-border {
  width: 2rem;
  height: 2rem;
}

.badge {
  font-size: 0.75rem;
  padding: 0.375rem 0.75rem;
}

.pagination {
  margin-bottom: 0;
}

.form-label {
  font-weight: 500;
  font-size: 0.875rem;
  margin-bottom: 0.5rem;
}

.alert {
  margin-bottom: 1rem;
  border: none;
  border-radius: 0.375rem;
}

.table-responsive {
  border-radius: 0.375rem;
}

.text-muted {
  color: #6c757d !important;
}

@media (max-width: 768px) {
  .table-responsive {
    font-size: 0.75rem;
  }
  
  .btn-sm {
    padding: 0.125rem 0.25rem;
    font-size: 0.625rem;
  }
  
  .card-header {
    padding: 0.75rem 1rem;
  }
  
  .card-header h4 {
    font-size: 1.1rem;
  }
}
```

## 📋 Exemplo Completo

### Entrada:
```
Table: Usuario
Entity: Usuario
Display Columns: Nome, Email, DataCriacao, Ativo
Searchable Columns: Nome, Email, DataCriacao
```

### Configuração de Colunas:
```typescript
displayColumns = [
  { field: 'nome', label: 'Nome', type: 'string' },
  { field: 'email', label: 'Email', type: 'string' },
  { field: 'dataCriacao', label: 'Data Criação', type: 'datetime' },
  { field: 'ativo', label: 'Ativo', type: 'boolean' }
];
```

## 🎯 Regras de Geração

### Nomenclatura
- **Component**: `[NomeEntidade]ListComponent`
- **Selector**: `app-[nome-entidade]-list`
- **File**: `[nome-entidade]-list.component.ts/html/css`

### Campos de Filtro Automáticos
Baseado no tipo da coluna:
- **String**: Input text com placeholder
- **Date/DateTime**: Input date (início e fim)
- **Boolean**: Select com opções Sim/Não/Todos
- **Number**: Input number com range
- **Enum**: Select com opções do enum

### Formatação de Dados
- **Date**: `dd/MM/yyyy`
- **DateTime**: `dd/MM/yyyy HH:mm`
- **Boolean**: Badge colorido (Sim/Não)
- **Number**: Formatação com locale
- **Currency**: Formatação monetária

### Funcionalidades Obrigatórias
1. **Listagem** com dados da API
2. **Filtros** baseados na estrutura
3. **Paginação** completa
4. **Loading** state
5. **Error** handling
6. **Confirmação** de exclusão
7. **Links** para edição
8. **Responsividade** mobile

## ✅ Checklist de Validação

- [ ] Component class criada corretamente
- [ ] Template HTML completo
- [ ] CSS responsivo
- [ ] Filtros baseados na estrutura
- [ ] Paginação implementada
- [ ] Loading e error states
- [ ] Confirmação de exclusão
- [ ] Links de navegação
- [ ] Formatação de dados apropriada
- [ ] Mobile-friendly