import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { CommonSharedModule } from '../common/common-shared.module';
import { ShellModule } from '../shell/shell.module';
import { ReactiveFormsModule } from '@angular/forms';

export const authenticationRoutes: Routes = [
  {
    path: '',
    redirectTo: 'signup',
    pathMatch: 'full'
  },
  {
    path: 'signin',
    component: SigninComponent
  },
  {
    path: 'signup',
    component: SignupComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  }
];

@NgModule({
  declarations: [
    SignupComponent,
    SigninComponent,
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    CommonSharedModule,
    ShellModule,
    ReactiveFormsModule,
    RouterModule.forChild(authenticationRoutes)
  ]
})
export class AuthenticationModule { }
