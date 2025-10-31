import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, } from 'rxjs';
import { LoggedUserModel } from './logged-user.model';

@Injectable({
  providedIn: 'root',
})

export class AuthenticationService {

  loggedUserSubject: BehaviorSubject<LoggedUserModel>;
  loggedUser: LoggedUserModel;

  constructor() {
    this.setUser(); // for the purpose of this example we initialize it with a default logged user
    this.loggedUserSubject = new BehaviorSubject(this.loggedUser);
  }

  isAuthenticated() {
    return this.loggedUserSubject.asObservable();
  }

  // tslint:disable-next-line:max-line-length
  setUser(name: string = 'Alex', lastname: string = 'Martins', image: string = '/assets/imgs/users/user-8.jpeg', email: string = 'alex.martins@example.com') {
    // this sets a default user for the template
    this.loggedUser = new LoggedUserModel();
    this.loggedUser.name = name;
    this.loggedUser.lastname = lastname;
    this.loggedUser.image = image;
    this.loggedUser.email = email;
  }

  signin(email: string, password: string): Observable<any> {
    this.setUser();
    // your log in logic should go here
    this.loggedUserSubject.next(this.loggedUser);
    return of(true);
  }

  signup(name: string, lastname: string, email: string, password: string): Observable<any> {
    this.setUser();
    // your signup logic should go here
    this.loggedUserSubject.next(this.loggedUser);
    return of(true);
  }

  logout(): Observable<any> {
    // this.loggedUser = null;
    // your log out logic should go here
    // this.loggedUserSubject.next(null);
    return of(true);
  }

  recoverPassword(email: any): Observable<any> {
    this.setUser();
    // your recover password login should go here
    this.loggedUserSubject.next(this.loggedUser);
    return of(true);
  }

}
