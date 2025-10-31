import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Route, RouterModule } from '@angular/router';

import { DragDropModule } from '@angular/cdk/drag-drop';

import { RegularTablesComponent } from './regular-tables/regular-tables.component';
import { RegularTablesResolver, ExtendedTablesResolver, SmartTablesResolver, ExternalFiltersTableResolver } from './tables.resolver';
import { TableDataService } from './table-data.service';
import { ExtendedTablesComponent } from './extended-tables/extended-tables.component';
import { SmartTablesComponent } from './smart-tables/smart-tables.component';
import { ExternalFiltersTableComponent } from './external-filters-table/external-filters-table.component';
import { CommonSharedModule } from '../common/common-shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

export const tablesRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'regular-tables',
    pathMatch: 'full'
  },
  {
    path: 'regular-tables',
    component: RegularTablesComponent,
    resolve: {
      tableData : RegularTablesResolver
    }
  },
  {
    path: 'extended-tables',
    component: ExtendedTablesComponent,
    resolve: {
      tableData : ExtendedTablesResolver
    }
  },
  {
    path: 'smart-tables',
    component: SmartTablesComponent,
    resolve: {
      tableData : SmartTablesResolver
    }
  },
  {
    path: 'external-filters-tables',
    component: ExternalFiltersTableComponent,
    resolve: {
      tableData : ExternalFiltersTableResolver
    }
  }
];

@NgModule({
  declarations: [
    RegularTablesComponent,
    ExtendedTablesComponent,
    SmartTablesComponent,
    ExternalFiltersTableComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    DragDropModule,
    RouterModule.forChild(tablesRoutes)
  ],
  providers: [
    TableDataService,
    RegularTablesResolver,
    ExtendedTablesResolver,
    SmartTablesResolver,
    ExternalFiltersTableResolver
  ]
})

export class TablesModule { }
