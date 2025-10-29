import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { EmptyLayoutComponent } from './core/layouts/empty/empty-layout.component';
import { DynamicLayoutComponent } from './core/layouts/dynamic/dynamic-layout.component';

const routes: Routes = [
  /*
    Default route
  */
  { path: '', redirectTo: '/dashboards/ecommerce', pathMatch: 'full' },
  // { path: '', redirectTo: '/authentication/signup', pathMatch: 'full' },
  /*
    Auth routes + layout
  */
  {
    path: '',
    component: EmptyLayoutComponent,
    children: [
      {
        path: 'authentication',
        loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
      }
    ]
  },
  /*
    Main routes + dynamic layouts
  */
  {
    path: '',
    component: DynamicLayoutComponent,
    children: [
      {
        path: 'charts',
        loadChildren: () => import('./charts/charts.module').then(m => m.ChartsModule)
      },
      {
        path: 'tables',
        loadChildren: () => import('./tables/tables.module').then(m => m.TablesModule)
      },
      {
        path: 'forms',
        loadChildren: () => import('./forms/forms.module').then(m => m.FormsModule)
      },
      {
        path: 'utilities',
        loadChildren: () => import('./utilities/utilities.module').then(m => m.UtilitiesModule)
      },
      {
        path: 'components',
        loadChildren: () => import('./components/components.module').then(m => m.ComponentsModule)
      },
      {
        path: 'dashboards',
        loadChildren: () => import('./dashboards/dashboards.module').then(m => m.DashboardsModule)
      },
      // { path: 'aluno', loadChildren: () => import('./aluno/aluno.module').then(m => m.AlunoModule) }
      // { path: 'alunostatus', loadChildren: () => import('./alunostatus/alunostatus.module').then(m => m.AlunoStatusModule) },
    ]
  },
  /*
    Undefined routes (should redirect to a 404 page)
  */
  { path: '**', redirectTo: 'charts' }
];

@NgModule({
  // Only call RouterModule.forRoot() in the root AppRoutingModule (or the AppModule if that's where you register
  // top level application routes). In any other module, you must call the RouterModule.forChild method to register additional routes.
  imports: [
    RouterModule.forRoot(routes, {
      // If you want to preload all lazy routes when the app loads, uncomment the following line
      // preloadingStrategy: PreloadAllModules,
      onSameUrlNavigation: 'reload'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
