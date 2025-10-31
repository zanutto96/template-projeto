import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import Swal from 'sweetalert2';
import { AlunoService } from '../aluno.service';
import { MatDialog } from '@angular/material/dialog';
import { AlunoRegisterComponent } from '../aluno-register/aluno-register.component';
import { TableConfig, TableColumn } from '../../shared/generic-table/generic-table.component';
import { SharedModule } from '../../shared/shared.module';
import { Aluno } from './../aluno.model';

@Component({
  selector: 'aluno-list',
  templateUrl: './aluno-list.component.html',
  styleUrls: ['./aluno-list.component.scss'],
  imports: [
    SharedModule
  ]
})
export class AlunoListComponent implements OnInit {

  @Output() edit = new EventEmitter<any>();

  public list: any = [];
  public loading: boolean = false;
  public vm: any = {};
  public filters: any = {};
  public debounceTime = null;

  public tableConfig: TableConfig = {
    columns: [
      {
        field: 'alunoFormacaoId',
        header: 'AlunoFormacaoId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'alunoStatus.descricao',
        header: 'AlunoStatusId',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'areaAtuacaoEmpresa',
        header: 'AreaAtuacaoEmpresa',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'areaAtuacaoId',
        header: 'AreaAtuacaoId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'bairro',
        header: 'Bairro',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'cargoQueOcupa',
        header: 'CargoQueOcupa',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'cEP',
        header: 'CEP',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'comentario',
        header: 'Comentario',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'cPF',
        header: 'CPF',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'dataAlteracao',
        header: 'DataAlteracao',
        type: 'date',
        sortable: true,
        filterable: true
      },
      {
        field: 'dataCadastro',
        header: 'DataCadastro',
        type: 'date',
        sortable: true,
        filterable: true
      },
      {
        field: 'dataNascimento',
        header: 'DataNascimento',
        type: 'date',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehEmail',
        header: 'EhEmail',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehExAluno',
        header: 'EhExAluno',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehFalecido',
        header: 'EhFalecido',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehMalaDireta',
        header: 'EhMalaDireta',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehMkt',
        header: 'EhMkt',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'ehSMS',
        header: 'EhSMS',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'email',
        header: 'Email',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'endereco',
        header: 'Endereco',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'enderecoComplemento',
        header: 'EnderecoComplemento',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'enderecoNumero',
        header: 'EnderecoNumero',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'estado',
        header: 'Estado',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'estadoCivilId',
        header: 'EstadoCivilId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'localNascimento',
        header: 'LocalNascimento',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nivelFormacaoId',
        header: 'NivelFormacaoId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomeCompleto',
        header: 'NomeCompleto',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomeCracha',
        header: 'NomeCracha',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomeEmpresaOndeTrabalha',
        header: 'NomeEmpresaOndeTrabalha',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomeMae',
        header: 'NomeMae',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomePai',
        header: 'NomePai',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'nomeSocial',
        header: 'NomeSocial',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'rG',
        header: 'RG',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'sexoId',
        header: 'SexoId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'telCelular',
        header: 'TelCelular',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'telComercial',
        header: 'TelComercial',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'telResidencial',
        header: 'TelResidencial',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'temWhatsApp',
        header: 'TemWhatsApp',
        type: 'boolean',
        sortable: true,
        filterable: true
      },
      {
        field: 'uRLFotoAluno',
        header: 'URLFotoAluno',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'uRLInstagran',
        header: 'URLInstagran',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'uRLLinkedin',
        header: 'URLLinkedin',
        type: 'text',
        sortable: true,
        filterable: true
      },
      {
        field: 'usuarioAlteracaoId',
        header: 'UsuarioAlteracaoId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'usuarioCadastroId',
        header: 'UsuarioCadastroId',
        type: 'numeric',
        sortable: true,
        filterable: true
      },
      {
        field: 'usuarioId',
        header: 'UsuarioId',
        type: 'numeric',
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
              tooltip: 'Editar aluno',
              cssClass: 'btn btn-sm btn-outline-primary me-1'
            },
            {
              id: 'delete',
              icon: 'fas fa-trash',
              tooltip: 'Excluir aluno',
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

  private service: AlunoService = inject(AlunoService);

  constructor(
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    console.log('AlunoListComponent initialized');
    this.getData();
  }

  public currentFilters: any = {};

  getData(filters: any = {}) {
    this.loading = true;
    this.service.getAluno(filters).subscribe({
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
        this.onEdit(event.row.alunoId);
        break;
      case 'delete':
        this.onDelete(event.row.alunoId);
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
          alunoId: id
        };
        this.service.deleteAluno(model).subscribe({
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
    const dialogRef = this.dialog.open(AlunoRegisterComponent, { data: { id: id } });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  onAdd() {
    const dialogRef = this.dialog.open(AlunoRegisterComponent, { data: {}, width: '80vw' });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getData();
      }
    });
  }

  onPageChanged(pageConfig: any) {
    // Manter filtros atuais e adicionar configuração de página
    let modelFilter = {
      ...this.currentFilters,
      pageIndex: pageConfig.pageIndex,
      pageSize: pageConfig.pageSize
    };
    
    this.service.getAluno(modelFilter).subscribe({
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
    // Combinar filtros atuais com ordenação
    const filters = {
      ...this.currentFilters,
      "OrderBy": sortEvent.active,
      "OrderByType": sortEvent.direction == "desc" ? "OrderByDescending" : "OrderBy",
      pageIndex: 0, // Resetar para primeira página quando ordenar
      pageSize: this.tableConfig.pageSize || 10
    };
    
    this.getData(filters);
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
