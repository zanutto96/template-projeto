import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AlunoStatusRoutingModule } from './alunostatus-routing.module';
import { AlunoStatusComponent } from './alunostatus.component';

import { AlunoStatusListComponent } from './alunostatus-list/alunostatus-list.component';
import { AlunoStatusRegisterComponent } from './alunostatus-register/alunostatus-register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { CommonSharedModule } from '../../common/common-shared.module';

@NgModule({
  declarations: [
  ],
  imports: [
    AlunoStatusComponent,
    AlunoStatusListComponent,
    AlunoStatusRegisterComponent,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AlunoStatusRoutingModule,
    CommonSharedModule,
  ]
})
export class AlunoStatusModule { }
