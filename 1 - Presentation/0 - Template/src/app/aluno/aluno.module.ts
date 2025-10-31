import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AlunoRoutingModule } from './aluno-routing.module';
import { AlunoComponent } from './aluno.component';

import { AlunoListComponent } from './aluno-list/aluno-list.component';
import { AlunoRegisterComponent } from './aluno-register/aluno-register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonSharedModule } from '../common/common-shared.module';

import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';

@NgModule({
  declarations: [
  ],
  imports: [
    AlunoComponent,
    AlunoListComponent,
    AlunoRegisterComponent,
    AlunoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    CommonSharedModule,
  ]
})
export class AlunoModule { }
