import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EcommerceDashboardComponent } from './ecommerce-dashboard/ecommerce-dashboard.component';
import { Route, RouterModule } from '@angular/router';

// echarts - lib: https://github.com/xieziyu/ngx-echarts
import { NgxEchartsModule } from 'ngx-echarts';
import { DashboardsService } from './dashboards.service';
import { CrmDashboardComponent } from './crm-dashboard/crm-dashboard.component';
import { EcommerceDashboardResolver, CrmDashboardResolver } from './dashboards.resolver';
import { CommonSharedModule } from '../common/common-shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ShellModule } from '../shell/shell.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

export const dashboardRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'ecommerce',
    pathMatch: 'full'
  },
  {
    path: 'ecommerce',
    component: EcommerceDashboardComponent,
    resolve: {
      data: EcommerceDashboardResolver
    }
  },
  {
    path: 'crm',
    component: CrmDashboardComponent,
    resolve: {
      data: CrmDashboardResolver
    }
  }
];

@NgModule({
  declarations: [EcommerceDashboardComponent, CrmDashboardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(dashboardRoutes),
    NgxEchartsModule.forChild(),
    CommonSharedModule,
    MatPaginatorModule,
    ShellModule,
    MatTableModule,
    MatSortModule,
    NgbModule,
  ],
  providers: [
    DashboardsService,
    EcommerceDashboardResolver,
    CrmDashboardResolver
  ]
})
export class DashboardsModule { }
