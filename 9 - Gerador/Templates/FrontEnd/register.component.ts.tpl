import { Component, EventEmitter, Inject, inject, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { #Entity#Service } from '../#EntityLowerCase#.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { #Entity# } from './../#entity#.model';

@Component({
  selector: '#EntityLowerCase#-register',
  templateUrl: './#EntityLowerCase#-register.component.html',
  styleUrls: ['./#EntityLowerCase#-register.component.scss'],
  imports: [
    ReactiveFormsModule,
    CommonModule
  ]
})
export class #Entity#RegisterComponent implements OnInit {

  @Input() id: any = 0;
  @Output() response = new EventEmitter();

  public form: FormGroup;
  public model: any = {};
  public showSelect = false;

    private service: #Entity#Service = inject(#Entity#Service);

  constructor(
    public dialogRef: MatDialogRef<#Entity#RegisterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.form = new FormGroup(this.getFormControls());
  }

  ngOnInit(): void {
    this.getData();
  }

  getFormControls(moreFormControls?: any) {
    var formControls = Object.assign({
      #EntityFormControls#
    }, moreFormControls || {});
    return formControls;
  }

  onSave() {
    if (this.form.valid) {
      this.model = { ...this.model, ...this.form.value };

      if (this.model.#EntityRepositoryPrimaryKeyFront#) {
        this.service.update#Entity#(this.model).subscribe({
          next: (data: any) => {
            this.dialogRef.close(true);
          },
          error: (error) => {
            console.error('Erro ao atualizar:', error);
          }
        });
      } else {
        this.service.save#Entity#(this.model).subscribe({
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
    var filters: Partial<#Entity#> = {
      #EntityRepositoryPrimaryKeyFront#: this.data?.id || this.id
    };
    if (filters.#EntityRepositoryPrimaryKeyFront# > 0) {
      this.service.get#Entity#ById(filters).subscribe({
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
