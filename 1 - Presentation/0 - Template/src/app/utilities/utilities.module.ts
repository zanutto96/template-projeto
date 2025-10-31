import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications/notifications.component';
import { Route, RouterModule } from '@angular/router';
import { ModalsComponent } from './modals/modals.component';
import { AlertsComponent } from './alerts/alerts.component';
import { AutocompleteComponent } from './autocomplete/autocomplete.component';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { CreateAccountModalComponent } from './modals/templates/create-account-modal/create-account-modal.component';
import { SignInModalComponent } from './modals/templates/sign-in-modal/sign-in-modal.component';
import { UtilitiesService } from './utilities.service';
import { CommonSharedModule } from '../common/common-shared.module';
import { ShellModule } from '../shell/shell.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

export const notificationsRoutes:Route[] = [
  {
    path: '',
    redirectTo: 'notifications',
    pathMatch: 'full'
  },
  {
    path: 'notifications',
    component: NotificationsComponent
  },
  {
    path: 'alerts',
    component: AlertsComponent
  },
  {
    path: 'modals',
    component: ModalsComponent
  },
  {
    path: 'file-uploader',
    component: FileUploaderComponent
  },
  {
    path: 'autocomplete',
    component: AutocompleteComponent
  }
];

@NgModule({
  declarations: [
    NotificationsComponent,
    ModalsComponent,
    AlertsComponent,
    AutocompleteComponent,
    FileUploaderComponent,
    CreateAccountModalComponent,
    SignInModalComponent
  ],
  providers: [
    UtilitiesService
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CommonSharedModule,
    MatAutocompleteModule,
    ShellModule,
    NgbModule,
    RouterModule.forChild(notificationsRoutes)
  ]
})
export class UtilitiesModule { }
