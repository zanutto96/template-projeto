import { Component, EventEmitter, Inject, inject, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AlunoService } from '../aluno.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Aluno } from './../aluno.model';
import { CommonSharedModule } from '../../../common/common-shared.module';

@Component({
  selector: 'aluno-register',
  templateUrl: './aluno-register.component.html',
  styleUrls: ['./aluno-register.component.scss'],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    CommonSharedModule
  ]
})
export class AlunoRegisterComponent implements OnInit {

  @Input() id: any = 0;
  @Output() response = new EventEmitter();

  public form: FormGroup;
  public model: any = {};
  public showSelect = false;

    private service: AlunoService = inject(AlunoService);

  constructor(
    public dialogRef: MatDialogRef<AlunoRegisterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = new FormGroup(this.getFormControls());
  }

  ngOnInit(): void {
    this.getData();
  }

  getFormControls(moreFormControls?: any) {
    var formControls = Object.assign({
      alunoFormacaoId: new FormControl(),
      alunoId: new FormControl(),
      alunoStatusId: new FormControl(),
      areaAtuacaoEmpresa: new FormControl(),
      areaAtuacaoId: new FormControl(),
      bairro: new FormControl(),
      cargoQueOcupa: new FormControl(),
      cEP: new FormControl(),
      comentario: new FormControl(),
      cPF: new FormControl(),
      dataAlteracao: new FormControl(),
      dataCadastro: new FormControl(),
      dataNascimento: new FormControl(),
      ehEmail: new FormControl(),
      ehExAluno: new FormControl(),
      ehFalecido: new FormControl(),
      ehMalaDireta: new FormControl(),
      ehMkt: new FormControl(),
      ehSMS: new FormControl(),
      email: new FormControl(),
      endereco: new FormControl(),
      enderecoComplemento: new FormControl(),
      enderecoNumero: new FormControl(),
      estado: new FormControl(),
      estadoCivilId: new FormControl(),
      localNascimento: new FormControl(),
      nivelFormacaoId: new FormControl(),
      nomeCompleto: new FormControl(),
      nomeCracha: new FormControl(),
      nomeEmpresaOndeTrabalha: new FormControl(),
      nomeMae: new FormControl(),
      nomePai: new FormControl(),
      nomeSocial: new FormControl(),
      rG: new FormControl(),
      sexoId: new FormControl(),
      telCelular: new FormControl(),
      telComercial: new FormControl(),
      telResidencial: new FormControl(),
      temWhatsApp: new FormControl(),
      uRLFotoAluno: new FormControl(),
      uRLInstagran: new FormControl(),
      uRLLinkedin: new FormControl(),
      usuarioAlteracaoId: new FormControl(),
      usuarioCadastroId: new FormControl(),
      usuarioId: new FormControl(),

    }, moreFormControls || {});
    return formControls;
  }

  onSave() {
    if (this.form.valid) {
      this.model = { ...this.model, ...this.form.value };

      if (this.model.alunoId) {
        this.service.updateAluno(this.model).subscribe({
          next: (data: any) => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Erro ao atualizar:', error);
          }
        });
      } else {
        this.service.saveAluno(this.model).subscribe({
          next: (data: any) => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Erro ao salvar:', error);
          }
        });
      }
    }
  }

  getData() {
    var filters: Partial<Aluno> = {
      alunoId: this.data?.id || this.id
    };
    if (filters.alunoId > 0) {
      this.service.getAlunoById(filters).subscribe({
        next: (result: any) => {
          // Verificar se a resposta tem a estrutura nova ou antiga
          if (result && result.data) {
            this.model = result.data;
          } else if (result && result.result && result.result.data) {
            this.model = result.result.data;
          } else {
            this.model = result;
          }
          this.form.patchValue(this.model);
          this.showSelect = true;
        },
        error: (error) => {
          console.error('Erro ao carregar dados:', error);
          this.showSelect = true;
        }
      });
    } else {
      this.showSelect = true;
    }
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }
}
