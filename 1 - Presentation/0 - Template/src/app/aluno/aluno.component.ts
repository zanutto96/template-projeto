import { Component, OnInit } from '@angular/core';

import { AlunoRegisterComponent } from './aluno-register/aluno-register.component';
import { AlunoListComponent } from './aluno-list/aluno-list.component';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-aluno',
  templateUrl: './aluno.component.html',
  styleUrls: ['./aluno.component.scss'],
  imports: [
    CommonModule,
    AlunoListComponent
  ]
})
export class AlunoComponent implements OnInit {
  
  public id: any;
  
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void { }

}


