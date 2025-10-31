import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import Swal from 'sweetalert2';
import { AlunoStatusService } from '../alunostatus.service';
import { MatDialog } from '@angular/material/dialog';
import { AlunoStatusRegisterComponent } from '../alunostatus-register/alunostatus-register.component';
import { TableConfig, TableColumn } from '../../shared/generic-table/generic-table.component';
import { SharedModule } from '../../shared/shared.module';
import { AlunoStatus } from './../alunostatus.model';

@Component({
  selector: 'alunostatus-list',
  templateUrl: './alunostatus-list.component.html',
  styleUrls: ['./alunostatus-list.component.scss'],
  imports: [
    SharedModule
  ]
})
export class AlunoStatusListComponent implements OnInit {

  @Output() edit = new EventEmitter<any>();

  public list: any = [];
  public loading: boolean = false;
  public vm: any = {};
  public filters: any = {};
  public debounceTime = null;

  public tableConfig: TableConfig = {
    columns: [
      {
        field: 'descricao',
        header: 'Descricao',
        type: 'text',
        sortable: true,
        filterable: true
      },

      {
        field: 'actions',
        header: 'Ações',
        type: 'actions',
        sortable: false,
        filterable: false,
        config: {
          actions: [
            {
              id: 'edit',
              icon: 'fas fa-edit',
              tooltip: 'Editar alunostatus',
              cssClass: 'btn btn-sm btn-outline-primary me-1'
            },
            {
              id: 'delete',
              icon: 'fas fa-trash',
              tooltip: 'Excluir alunostatus',
              cssClass: 'btn btn-sm btn-outline-danger'
            }
          ]
        }
      }
    ],
    paginate: true,
    pageSize: 10,
    pageSizeOptions: [5, 10, 25, 50],
    showFilters: true,
    filtersVisible: false
  };

  private service: AlunoStatusService = inject(AlunoStatusService);

  constructor(
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    console.log('AlunoStatusListComponent initialized');
    this.getData();
  }

  public currentFilters: any = {};

  getData(filters: any = null) {
    var filters = {
      ...filters
    };
    this.loading = true;
    this.service.getAlunoStatus(filters).subscribe({
      next: (result) => {
        // Verificar se a resposta tem a estrutura nova ou antiga
        if (result && result.data && result.data.dataList) {
          // Nova estrutura (após correção)
          this.vm = result.data;
          this.list = result.data.dataList;
        } else {
          // Fallback
          this.list = [];
          this.vm = { dataList: [] };
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Erro ao carregar dados:', error);
        this.list = [];
        this.vm = { dataList: [] };
        this.loading = false;
      }
    });
  }

  onActionClicked(event: { action: string; row: any }) {
    switch (event.action) {
      case 'edit':
        this.onEdit(event.row.alunoStatusId);
        break;
      case 'delete':
        this.onDelete(event.row.alunoStatusId);
        break;
    }
  }

  onDelete(id: any) {
    Swal.fire({
      title: 'Deseja apagar o registro?',
      text: 'Esta operação não poderá ser revertida!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sim, apagar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        let model = {
          alunoStatusId: id
        };
        this.service.deleteAlunoStatus(model).subscribe({
          next: () => {
            this.getData();
            Swal.fire(
              'Apagado!',
              'O registro foi removido com sucesso.',
              'success'
            );
          },
          error: (error) => {
            console.error('Erro ao excluir registro:', error);
            Swal.fire(
              'Erro!',
              'Não foi possível excluir o registro.',
              'error'
            );
          }
        });
      }
    });
  }

  onEdit(id: any) {
    const dialogRef = this.dialog.open(AlunoStatusRegisterComponent, { data: { id: id } });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  onAdd() {
    const dialogRef = this.dialog.open(AlunoStatusRegisterComponent, { data: {} });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  onPageChanged(pageConfig: any) {

    let modelFilter: Partial<AlunoStatus> = {
      pageIndex: pageConfig.pageIndex,
      pageSize: pageConfig.pageSize
    };

    this.service.getAlunoStatus(modelFilter).subscribe({
      next: (result) => {
        // Verificar se a resposta tem a estrutura nova ou antiga
        if (result && result.data && result.data.dataList) {
          this.vm = result.data;
          this.list = result.data.dataList;
        } else {
          this.list = [];
          this.vm = { dataList: [] };
        }
      },
      error: (error) => {
        console.error('Erro ao carregar página:', error);
        this.list = [];
        this.vm = { dataList: [] };
      }
    });
  }

  onSort(sortEvent: any) {
    this.getData({
      ...this.filters,
      "OrderBy": sortEvent.active,
      "OrderByType": sortEvent.direction == "desc" ? "OrderByDescending" : "OrderBy"
    })
  }
  onFilterChanged(filter: any) {
    // Aplicar debounce para evitar múltiplas requisições
    if (this.debounceTime) {
      clearTimeout(this.debounceTime);
    }
    
    this.debounceTime = setTimeout(() => {
      // Combinar filtros com paginação atual
      const filters = {
        ...filter,
        pageIndex: 0, // Resetar para primeira página quando filtrar
        pageSize: this.tableConfig.pageSize || 10
      };
      
      this.getData(filters);
    }, 500);
  }
}
