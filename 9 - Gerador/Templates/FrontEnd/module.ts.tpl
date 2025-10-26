import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { #Entity#RoutingModule } from './#EntityLowerCase#-routing.module';
import { #Entity#Component } from './#EntityLowerCase#.component'; 
 
import { #Entity#ListComponent } from './#EntityLowerCase#-list/#EntityLowerCase#-list.component';
import { #Entity#RegisterComponent } from './#EntityLowerCase#-register/#EntityLowerCase#-register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonSharedModule } from 'app/common/common-shared.module';

import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatRadioModule } from '@angular/material/radio';
import { UtilSharedModule } from 'app/util/util-shared.module';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [
    #Entity#Component,
    #Entity#ListComponent,
    #Entity#RegisterComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    #Entity#RoutingModule,
    CommonSharedModule,
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatRadioModule,
    UtilSharedModule,
    MatSortModule,
  ]
})
export class #Entity#Module { }
