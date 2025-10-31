import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialFormsComponent } from './material-forms/material-forms.component';
import { Route, RouterModule } from '@angular/router';
import { ExtendedControlsComponent } from './extended-controls/extended-controls.component';
import { ControlsAndValidationsComponent } from './controls-and-validations/controls-and-validations.component';
import { FormsService } from './forms.service';
import { MatCheckbox, MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ShellModule } from '../shell/shell.module';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { CommonSharedModule } from '../common/common-shared.module';
import { ImageShellComponent } from '../shell/image-shell/image-shell.component';

export const formsRoutes: Route[] = [
  {
    path: '',
    redirectTo: 'material-forms',
    pathMatch: 'full'
  },
  {
    path: 'material-forms',
    component: MaterialFormsComponent,
  },
  {
    path: 'controls-and-validations',
    component: ControlsAndValidationsComponent,
  },
  {
    path: 'extended-controls',
    component: ExtendedControlsComponent,
  },

];

@NgModule({
  declarations: [
    MaterialFormsComponent,
    ExtendedControlsComponent,
    ControlsAndValidationsComponent
  ],
  providers: [
    FormsService
  ],
  imports: [
    CommonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    ShellModule,
    MatProgressBarModule,
    NgbModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatCheckboxModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatIconModule,
    RouterModule.forChild(formsRoutes),
    CommonSharedModule,
    ImageShellComponent
  ]
})

export class FormsModule { }
