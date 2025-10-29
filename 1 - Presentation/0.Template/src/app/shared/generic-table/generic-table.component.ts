import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormControl } from '@angular/forms';

export type ColumnType = 
  | 'text' 
  | 'numeric' 
  | 'currency' 
  | 'date' 
  | 'checkbox' 
  | 'progress' 
  | 'boolean' 
  | 'thumbnail' 
  | 'actions';

export interface TableColumn {
  /** Identificador único da coluna */
  field: string;
  
  /** Título exibido no header */
  header: string;
  
  /** Tipo de dados da coluna */
  type: ColumnType;
  
  /** Se a coluna pode ser ordenada */
  sortable?: boolean;
  
  /** Se a coluna possui filtro */
  filterable?: boolean;
  
  /** Largura da coluna (ex: '100px', '20%') */
  width?: string;
  
  /** CSS classes customizadas */
  cssClass?: string;
  
  /** Configurações específicas por tipo */
  config?: {
    // Para tipo 'currency'
    currencySymbol?: string;
    currencyCode?: string;
    
    // Para tipo 'date'
    dateFormat?: string;
    
    // Para tipo 'thumbnail'
    thumbnailField?: string;
    aspectRatio?: { w: number; h: number };
    
    // Para tipo 'progress'
    progressColor?: string;
    showLabel?: boolean;
    
    // Para tipo 'actions'
    actions?: TableAction[];
  };
}

export interface TableAction {
  /** Identificador da ação */
  id: string;
  
  /** Ícone FontAwesome */
  icon: string;
  
  /** Tooltip */
  tooltip?: string;
  
  /** CSS class */
  cssClass?: string;
  
  /** Condição para mostrar a ação (recebe a row) */
  condition?: (row: any) => boolean;
}

export interface TableConfig {
  /** Colunas da tabela */
  columns: TableColumn[];
  
  /** Mostrar paginação */
  paginate?: boolean;
  
  /** Tamanho da página */
  pageSize?: number;
  
  /** Opções de tamanho de página */
  pageSizeOptions?: number[];
  
  /** Mostrar filtros */
  showFilters?: boolean;
  
  /** Filtros inicialmente visíveis */
  filtersVisible?: boolean;
  
  /** Permitir seleção de linhas */
  selectable?: boolean;
  
  /** CSS classes customizadas */
  cssClass?: string;
}

@Component({
  selector: 'app-generic-table',
  templateUrl: './generic-table.component.html',
  styleUrls: ['./generic-table.component.scss'],
  standalone: false
})
export class GenericTableComponent implements OnInit {
  @Input() data: any[] = [];
  @Input() config: TableConfig;
  
  @Output() actionClicked = new EventEmitter<{ action: string; row: any }>();
  @Output() rowClicked = new EventEmitter<any>();
  @Output() selectionChanged = new EventEmitter<any[]>();
  @Output() pageChanged = new EventEmitter<PageEvent>();

  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = [];
  filtersForm: FormGroup;
  filtersVisible = false;
  toggleFiltersLabel = 'Show filters';
  selectedRows = new Set<any>();

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  ngOnInit() {
    this.initializeTable();
    this.initializeFilters();
  }

  private initializeTable() {
    // Configurar colunas exibidas
    if (this.config?.selectable) {
      this.displayedColumns.push('select');
    }
    
    this.displayedColumns.push(...this.config.columns.map(col => col.field));
    
    // Inicializar datasource
    this.dataSource = new MatTableDataSource(this.data);
    
    // Configurar paginação
    if (this.config?.paginate !== false) {
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
      });
    }
    
    // Configurar ordenação
    setTimeout(() => {
      this.dataSource.sort = this.sort;
      
      // Custom sort para datas e números
      this.dataSource.sortingDataAccessor = (item, property) => {
        const column = this.config.columns.find(col => col.field === property);
        
        if (column?.type === 'date') {
          return new Date(item[property]);
        } else if (column?.type === 'numeric' || column?.type === 'currency') {
          return parseFloat(item[property]) || 0;
        }
        
        return item[property];
      };
    });
    
    // Definir visibilidade inicial dos filtros
    if (this.config?.filtersVisible !== undefined) {
      this.filtersVisible = this.config.filtersVisible;
      this.toggleFiltersLabel = this.filtersVisible ? 'Hide filters' : 'Show filters';
    }
  }

  private initializeFilters() {
    const formControls = {};
    
    // Criar controles de filtro para cada coluna filtrável
    this.config.columns.forEach(column => {
      if (column.filterable !== false) {
        formControls[column.field] = new FormControl('');
      }
    });
    
    // Adicionar campo de busca global
    formControls['globalSearch'] = new FormControl('');
    
    this.filtersForm = new FormGroup(formControls);
    
    // Aplicar filtros quando houver mudanças
    this.filtersForm.valueChanges.subscribe(() => this.applyFilters());
  }

  applyFilters() {
    const filterValues = this.filtersForm.value;
    
    this.dataSource.filterPredicate = (data: any, filter: string) => {
      const filters = JSON.parse(filter);
      
      // Filtro global
      if (filters.globalSearch) {
        const globalMatch = Object.keys(data).some(key => {
          const value = data[key]?.toString().toLowerCase() || '';
          return value.includes(filters.globalSearch.toLowerCase());
        });
        
        if (!globalMatch) {
          return false;
        }
      }
      
      // Filtros por coluna
      for (const key in filters) {
        if (key !== 'globalSearch' && filters[key]) {
          const dataValue = data[key]?.toString().toLowerCase() || '';
          const filterValue = filters[key].toString().toLowerCase();
          
          if (!dataValue.includes(filterValue)) {
            return false;
          }
        }
      }
      
      return true;
    };
    
    this.dataSource.filter = JSON.stringify(filterValues);
    
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  clearFilters() {
    this.filtersForm.reset();
    this.dataSource.filter = '';
  }

  toggleFilters() {
    this.filtersVisible = !this.filtersVisible;
    this.toggleFiltersLabel = this.filtersVisible ? 'Hide filters' : 'Show filters';
  }

  onRowClick(row: any) {
    this.rowClicked.emit(row);
  }

  onActionClick(action: string, row: any, event: Event) {
    event.stopPropagation();
    this.actionClicked.emit({ action, row });
  }

  toggleRowSelection(row: any) {
    if (this.selectedRows.has(row)) {
      this.selectedRows.delete(row);
    } else {
      this.selectedRows.add(row);
    }
    
    this.selectionChanged.emit(Array.from(this.selectedRows));
  }

  isRowSelected(row: any): boolean {
    return this.selectedRows.has(row);
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selectedRows.clear();
    } else {
      this.dataSource.data.forEach(row => this.selectedRows.add(row));
    }
    
    this.selectionChanged.emit(Array.from(this.selectedRows));
  }

  isAllSelected(): boolean {
    return this.selectedRows.size === this.dataSource.data.length;
  }

  isSomeSelected(): boolean {
    return this.selectedRows.size > 0 && !this.isAllSelected();
  }

  getColumnValue(row: any, column: TableColumn): any {
    return row[column.field];
  }

  shouldShowAction(action: TableAction, row: any): boolean {
    return action.condition ? action.condition(row) : true;
  }

  formatCurrency(value: number, column: TableColumn): string {
    const symbol = column.config?.currencySymbol || '$';
    const code = column.config?.currencyCode || 'USD';
    
    return `${symbol}${value.toFixed(2)}`;
  }

  formatDate(value: string | Date, column: TableColumn): string {
    const format = column.config?.dateFormat || 'MM/dd/yyyy';
    // Implementação simples - em produção usar DatePipe ou library como date-fns
    const date = new Date(value);
    return date.toLocaleDateString();
  }

  onPageChange(event: PageEvent) {
    this.pageChanged.emit(event);
  }

  getFilterType(column: TableColumn): string {
    switch (column.type) {
      case 'numeric':
      case 'currency':
        return 'number';
      case 'date':
        return 'date';
      case 'checkbox':
      case 'boolean':
        return 'checkbox';
      default:
        return 'text';
    }
  }
}
