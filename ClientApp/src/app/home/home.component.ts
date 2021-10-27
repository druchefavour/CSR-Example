import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../globals';
import config from '../app.config';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [Globals]
})
export class HomeComponent implements OnInit {
  searchResults: any = [];
  valid: boolean = true;
  verifyValid: boolean = true;
  showModal: boolean = false;
  showDl: boolean = true;
  showQuestion: boolean = true;
  showEmpty: boolean = false;
  userData: any;
  display: string = '';
  userLogin: string = '';
  securityQuestion: string = '';
  showMore: string = '';
  addUserURL: string = '';
  results: any[] = [];
  count: number = 1;

  filterForm = this.fb.group({
    login: '',
    email: '',
    firstName: '',
    lastName: '',
    ibisId: '',
    city: '',
    state: '',
    zipCode: '',
    drivers_license: ''
  });
  verifyForm = this.fb.group({
    driversLiscense: '',
    securityAnswer: ''
  });
  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json'
    })
  };

  constructor(
    private oauthService: OAuthService,
    private fb: FormBuilder,
    private http: HttpClient,
    private toastr: ToastrService,
    public globals: Globals,
    private route: Router
  ) {
    sessionStorage.setItem('id', '');
    sessionStorage.setItem('username', '');
    sessionStorage.setItem('email', '');
    sessionStorage.setItem('firstName', '');
    sessionStorage.setItem('lastName', '');
    sessionStorage.setItem('ibisid', '');
    sessionStorage.setItem('streetAddress', '');
    sessionStorage.setItem('city', '');
    sessionStorage.setItem('state', '');
    sessionStorage.setItem('zipCode', '');
    sessionStorage.setItem('primaryPhone', '');
    sessionStorage.setItem('status', '');
    sessionStorage.setItem('birthDate', '');
    sessionStorage.setItem('idproofing_status', '');
    sessionStorage.setItem('idproofing_score', '');
    sessionStorage.setItem('idproofing_LOA', '');
    this.addUserURL = config.config.addUserUrl;
  }

  /*  login() {
      this.oauthService.initImplicitFlow();
      // this.oauthService.silentRefresh();
    }
  
    logout() {
      this.oauthService.logOut();
    }*/

  get givenName() {
    const claims: any = this.oauthService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    console.warn(claims);
    return claims['name'];
  }

  ngOnInit() { }

  selectPage(i: number) {
    this.count = i;
    this.searchResults = this.results[this.count - 1];
  }

  nextPage() {
    this.count++;
    this.searchResults = this.results[this.count - 1];
  }

  prevPage() {
    this.count--;
    this.searchResults = this.results[this.count - 1];
  }

  onSubmit(): void {
    this.handleResults(this.filterForm.value);
  }

  handleResults(values: any) {
    this.showEmpty = false;
    this.searchResults = [];
    var check = 0;
    const { login, email, firstName, lastName, ibisId, city, state, zipCode, drivers_license } = values;
    if (firstName !== '') check++;
    if (lastName !== '') check++;
    if (login !== '') check++;
    if (email !== '') check++;
    if (ibisId !== '') check++;
    if (city !== '') check++;
    if (state !== '') check++;
    if (zipCode !== '') check++;
    // if (drivers_license !== '') check++;

    if (check >= 1) {
      this.http.post(`${this.globals.url}/api/moreUsers/searchParameters`, this.filterForm.value, this.httpOptions)
        .subscribe(
          (data: any) => {
            if (data.errorCode === undefined) {
              if (data.length !== 0) {
                data.forEach(el => {
                  if (el !== "") {
                    if (JSON.parse(el) !== null)
                      this.results.push(JSON.parse(el));
                  }
                });
                this.searchResults = this.results[0];
                this.valid = true;
              } else this.showEmpty = true;
            } else this.toastr.error(data.errorSummary, 'An error has occured!');
          },
          err => this.toastr.error('Please try again later', 'An error has occured!')
        )
    } else {
      this.searchResults = [];
      this.valid = false;
    };
  }

  getMore() {

  }

  redirectToUser(user) {
    sessionStorage.setItem('id', user.id);
    sessionStorage.setItem('username', user.profile.login);
    sessionStorage.setItem('email', user.profile.email);
    sessionStorage.setItem('firstName', user.profile.firstName);
    sessionStorage.setItem('lastName', user.profile.lastName);
    sessionStorage.setItem('ibisid', user.profile.ibisid);
    sessionStorage.setItem('driversLicense', user.profile.drivers_license);
    sessionStorage.setItem('streetAddress', user.profile.streetAddress);
    sessionStorage.setItem('city', user.profile.city);
    sessionStorage.setItem('state', user.profile.state);
    sessionStorage.setItem('zipCode', user.profile.zipCode);
    sessionStorage.setItem('primaryPhone', user.profile.primaryPhone);
    sessionStorage.setItem('status', user.status);
    sessionStorage.setItem('birthDate', user.profile.birthdate);
    sessionStorage.setItem('idproofing_status', user.profile.idproofing_status);
    sessionStorage.setItem('idproofing_score', user.profile.idproofing_score);
    sessionStorage.setItem('idproofing_LOA', user.profile.idproofing_LOA);
    this.route.navigate(['/view']);
  }
}
