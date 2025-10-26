import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { #Entity#Component } from './#EntityLowerCase#.component';

const routes: Routes = [
  { path: '', data: { title: "#Entity#" }, component: #Entity#Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class #Entity#RoutingModule { }
