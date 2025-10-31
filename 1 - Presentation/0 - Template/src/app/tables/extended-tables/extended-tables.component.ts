import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { ImageShellComponent } from '../../shell/image-shell/image-shell.component';
import { ShellModule } from '../../shell/shell.module';
import { NgbProgressbarModule, NgbPopoverModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-extended-tables',
  templateUrl: './extended-tables.component.html',
  styleUrls: [
    './extended-tables.component.scss',
  ],
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    ImageShellComponent,
    ShellModule,
    NgbProgressbarModule,
    NgbPopoverModule
  ]
})
export class ExtendedTablesComponent {

  displayedColumns: string[] = ['picture', 'name', 'status', 'progress', 'renewal_date', 'subscription', 'actions'];
  dataSource: any;

  constructor(private route: ActivatedRoute) {
    // tslint:disable-next-line:no-string-literal
    this.dataSource = route.snapshot.data['tableData'];
  }

}
