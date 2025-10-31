import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { #Entity#RoutingModule } from './#EntityLowerCase#-routing.module';
import { #Entity#Component } from './#EntityLowerCase#.component';

import { #Entity#ListComponent } from './#EntityLowerCase#-list/#EntityLowerCase#-list.component';
import { #Entity#RegisterComponent } from './#EntityLowerCase#-register/#EntityLowerCase#-register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
  declarations: [
  ],
  imports: [
    #Entity#Component,
    #Entity#ListComponent,
    #Entity#RegisterComponent,
    #Entity#RoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    CommonSharedModule,
  ]
})
export class #Entity#Module { }
