import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AlunoStatusComponent } from './alunostatus.component';

const routes: Routes = [
  { path: '', data: { title: "AlunoStatus" }, component: AlunoStatusComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlunoStatusRoutingModule { }
