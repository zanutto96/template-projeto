import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatChipsModule } from '@angular/material/chips';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSliderModule } from '@angular/material/slider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSidenavModule } from '@angular/material/sidenav';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// import { HttpClientModule } from '@angular/common/http';

import { ShellModule } from '../shell/shell.module';
import { FileUploaderDirective } from './file-uploader/file-uploader.directive';
import { ShowHidePasswordComponent } from './show-hide-password/show-hide-password.component';
import { ShowHideInputDirective } from './show-hide-password/show-hide-input.directive';
import { AlertTemplateComponent } from './alert-template/alert-template.component';
import { NotificationTemplateComponent } from './notification-template/notification-template.component';
import { GenericTableModule } from './generic-table/generic-table.module';

@NgModule({
  declarations: [
    FileUploaderDirective,
    ShowHidePasswordComponent,
    ShowHideInputDirective,
    AlertTemplateComponent,
    NotificationTemplateComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ShellModule,
    MatIconModule,
    MatDialogModule,
    MatSnackBarModule,
    MatTableModule,
    MatTabsModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
    MatMenuModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatCheckboxModule,
    MatInputModule,
    MatNativeDateModule,
    MatChipsModule,
    MatSelectModule,
    MatStepperModule,
    MatTooltipModule,
    MatAutocompleteModule,
    MatButtonToggleModule,
    MatSlideToggleModule,
    MatSliderModule,
    MatExpansionModule,
    MatSidenavModule,
    GenericTableModule,
    NgbModule
  ],
  exports: [
    // Re-export these modules to prevent repeated imports (see: https://angular.io/guide/ngmodule#re-exporting-other-modules)
    GenericTableModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    // HttpClientModule,
    ShellModule,
    RouterModule,
    FileUploaderDirective,
    ShowHidePasswordComponent,
    ShowHideInputDirective,
    AlertTemplateComponent,
    NotificationTemplateComponent,
    // angular material modules
    MatTableModule,
    MatTabsModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
    MatMenuModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatCheckboxModule,
    MatInputModule,
    MatNativeDateModule,
    MatChipsModule,
    MatSelectModule,
    MatStepperModule,
    MatTooltipModule,
    MatAutocompleteModule,
    MatButtonToggleModule,
    MatSlideToggleModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSliderModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,  // needed to use the MatIconRegistry to register our own icons
    // ng bootstrap modules
    NgbModule
  ]
})
export class SharedModule { }
