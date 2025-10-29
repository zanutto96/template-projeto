import { Component, OnInit } from '@angular/core';

import { #Entity#RegisterComponent } from './#EntityLowerCase#-register/#EntityLowerCase#-register.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-#EntityLowerCase#',
  templateUrl: './#EntityLowerCase#.component.html',
  styleUrls: ['./#EntityLowerCase#.component.scss']
})
export class #Entity#Component implements OnInit {
  currentView: any = 'showList';
  id: any;
  constructor(public dialog: MatDialog) { }

  ngOnInit(): void { }

  onEdit(id: any) { 
    this.id = id;
    this.currentView = 'showRegister';
  }

  onClose() {
    this.currentView = 'showList';
    this.id = 0;
  }

  onAdd() {
    const dialogRef = this.dialog.open(#Entity#RegisterComponent, { data: { } });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.currentView = "nada";
        setTimeout(() => {
          this.currentView = "showList"
        }, 500);
      }
    });
  }
}


