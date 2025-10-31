import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  standalone: false
})
export class SignupComponent implements OnInit {

  signupForm: FormGroup;

  constructor(
    public router: Router,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    this.signupForm = new FormGroup({
      firstname: new FormControl(''),
      lastname: new FormControl(''),
      email: new FormControl(''),
      country: new FormControl(''),
      password: new FormControl(''),
      confirmPassword: new FormControl(''),
      terms: new FormControl(false)
    });
  }

  onSignup() {
    const data = this.signupForm.value;

    this.authenticationService.signup(data.firstname, data.lastname, data.email, data.password)
    .subscribe (
      res => {
        // if signup success then:
        this.router.navigate(['dashboards/ecommerce']);
      },
      err => {
        // else if signup fails
        // show error
      }
    );
  }

}
