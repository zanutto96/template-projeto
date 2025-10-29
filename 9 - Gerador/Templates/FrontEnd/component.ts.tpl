import { Component, OnInit } from '@angular/core';

import { #Entity#RegisterComponent } from './#EntityLowerCase#-register/#EntityLowerCase#-register.component';
import { #Entity#ListComponent } from './#EntityLowerCase#-list/#EntityLowerCase#-list.component';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-#EntityLowerCase#',
  templateUrl: './#EntityLowerCase#.component.html',
  styleUrls: ['./#EntityLowerCase#.component.scss'],
  imports: [
    CommonModule,
    #Entity#ListComponent
  ]
})
export class #Entity#Component implements OnInit {
  
  public id: any;
  
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void { }

}


