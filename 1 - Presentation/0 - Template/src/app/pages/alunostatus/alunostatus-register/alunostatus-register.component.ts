import { Component, EventEmitter, Inject, inject, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AlunoStatusService } from '../alunostatus.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { AlunoStatus } from './../alunostatus.model';

@Component({
  selector: 'alunostatus-register',
  templateUrl: './alunostatus-register.component.html',
  styleUrls: ['./alunostatus-register.component.scss'],
  imports: [
    ReactiveFormsModule,
    CommonModule
  ]
})
export class AlunoStatusRegisterComponent implements OnInit {

  @Input() id: any = 0;
  @Output() response = new EventEmitter();

  public form: FormGroup;
  public model: any = {};
  public showSelect = false;

    private service: AlunoStatusService = inject(AlunoStatusService);

  constructor(
    public dialogRef: MatDialogRef<AlunoStatusRegisterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = new FormGroup(this.getFormControls());
  }

  ngOnInit(): void {
    this.getData();
  }

  getFormControls(moreFormControls?: any) {
    var formControls = Object.assign({
      alunoStatusId: new FormControl(),
      descricao: new FormControl(),

    }, moreFormControls || {});
    return formControls;
  }

  onSave() {
    if (this.form.valid) {
      this.model = { ...this.model, ...this.form.value };

      if (this.model.alunoStatusId) {
        this.service.updateAlunoStatus(this.model).subscribe({
          next: (data: any) => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Erro ao atualizar:', error);
          }
        });
      } else {
        this.service.saveAlunoStatus(this.model).subscribe({
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
    var filters: Partial<AlunoStatus> = {
      alunoStatusId: this.data?.id || this.id
    };
    if (filters.alunoStatusId > 0) {
      this.service.getAlunoStatusById(filters).subscribe({
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
