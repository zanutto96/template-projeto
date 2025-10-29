import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
  standalone: false
})
export class ForgotPasswordComponent implements OnInit {

  recoverPasswordForm: FormGroup;

  constructor(
    public router: Router,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    this.recoverPasswordForm = new FormGroup({
      email: new FormControl('')
    });
  }

  onRecoverPassword() {
    this.authenticationService.recoverPassword(this.recoverPasswordForm.value.email)
    .subscribe (
      res => {
        // if recoverpassword success then:
        this.router.navigate(['dashboards/ecommerce']);
      },
      err => {
        // else if recoverpassword fails
        // show error
      }
    );
  }

}
