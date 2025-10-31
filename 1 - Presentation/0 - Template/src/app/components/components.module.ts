import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListsComponent } from './lists/lists.component';
import { CardsComponent } from './cards/cards.component';
import { Route, RouterModule } from '@angular/router';
import { GenericTableExampleComponent } from './generic-table-example/generic-table-example.component';

import { CardsResolver, ListsResolver } from './components.resolver';
import { ComponentsService } from './components.service';
import { CommonSharedModule } from '../common/common-shared.module';

export const componentsRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'lists',
    pathMatch: 'full'
  },
  {
    path: 'lists',
    component: ListsComponent,
    resolve: {
      data : ListsResolver
    }
  },
  {
    path: 'cards',
    component: CardsComponent,
    resolve: {
      data : CardsResolver
    }
  },
  {
    path: 'generic-table',
    component: GenericTableExampleComponent
  }
];

@NgModule({
  declarations: [
    ListsComponent, 
    CardsComponent, 
    GenericTableExampleComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    RouterModule.forChild(componentsRoutes)
  ],
  providers: [
    ComponentsService,
    CardsResolver,
    ListsResolver
  ]
})
export class ComponentsModule { }
