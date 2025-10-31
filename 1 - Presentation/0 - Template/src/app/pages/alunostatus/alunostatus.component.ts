import { Component, OnInit } from '@angular/core';

import { AlunoStatusRegisterComponent } from './alunostatus-register/alunostatus-register.component';
import { AlunoStatusListComponent } from './alunostatus-list/alunostatus-list.component';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alunostatus',
  templateUrl: './alunostatus.component.html',
  styleUrls: ['./alunostatus.component.scss'],
  imports: [
    CommonModule,
    AlunoStatusListComponent
  ]
})
export class AlunoStatusComponent implements OnInit {
  
  public id: any;
  
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void { }

}


