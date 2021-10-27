import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../globals';

import { HomeComponent } from '../home/home.component';

@Component({
  selector: 'app-verify',
  templateUrl: 'verify.component.html',
  styleUrls: ['verify.component.css']
})

export class VerifyComponent implements OnInit {
  @ViewChild('closeButton') closeButton: any;
  @Input() user!: HomeComponent;

  verifyValid: boolean = true;
  showModal: boolean = false;
  showDl: boolean = true;
  showQuestion: boolean = true;
  userData: any;
  display: string = '';
  userLogin: string = '';
  driversLicense: string = '';
  securityQuestion: string = '';
  tempPassword: string = '';

  verifyForm = this.fb.group({
    option: '',
    driversLiscense: '',
    securityAnswer: ''
  })
  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json'
    })
  };

  constructor(
    private fb: FormBuilder,
    private route: Router,
    private http: HttpClient,
    public globals: Globals,
    private toastr: ToastrService
  ) { }

  ngOnInit() { }

  handleShow(user: any) {
    console.log(user);
    this.showDl = true;
    // this.showQuestion = true;
    this.userData = user;
    if (user.profile.drivers_license === undefined || user.profile.drivers_license === '') this.showDl = false;
    else this.driversLicense = user.profile.drivers_license;
    // if (user.credentials.recovery_question === undefined || user.credentials.recovery_question === '') this.showQuestion = false;
    // else this.securityQuestion = user.credentials.recovery_question.question;
    this.userLogin = user.profile.login;
    this.verifyForm = this.fb.group({
      driversLiscense: '',
      // securityAnswer: ''
    });
    // this.display = (this.showDl || (this.showDl && !this.showQuestion)) ? "dl" : "sq";
    this.showModal = true;
  }

  handleClose() {
    this.userData = {};
    this.userLogin = "";
    this.securityQuestion = "";
    this.showModal = false;
  }

  handleVerifySubmit(): void {
    this.verifyValid = true;
    // if (this.display === "dl") {
      console.log(this.userData);
      sessionStorage.setItem('id', this.userData.id);
      sessionStorage.setItem('username', this.userData.profile.login);
      sessionStorage.setItem('email', this.userData.profile.email);
      sessionStorage.setItem('firstName', this.userData.profile.firstName);
      sessionStorage.setItem('lastName', this.userData.profile.lastName);
      sessionStorage.setItem('ibisid', this.userData.profile.ibisid);
      sessionStorage.setItem('driversLicense', this.userData.profile.drivers_license);
      sessionStorage.setItem('streetAddress', this.userData.profile.streetAddress);
      sessionStorage.setItem('city', this.userData.profile.city);
      sessionStorage.setItem('state', this.userData.profile.state);
      sessionStorage.setItem('zipCode', this.userData.profile.zipCode);
      sessionStorage.setItem('primaryPhone', this.userData.profile.primaryPhone);
      sessionStorage.setItem('status', this.userData.status);
      sessionStorage.setItem('birthDate', this.userData.profile.birthdate);
      sessionStorage.setItem('idproofing_status', this.userData.profile.idproofing_status);
      sessionStorage.setItem('idproofing_score', this.userData.profile.idproofing_score);
      sessionStorage.setItem('idproofing_LOA', this.userData.profile.idproofing_LOA);
      this.closeButton.nativeElement.click();
      this.route.navigate(['/view']);
    // } else {
    //   var obj = { 'Id': this.userData.id, 'Question': this.userData.credentials.recovery_question.question,'Answer': this.verifyForm.value.securityAnswer }
    //   this.http.post(`${this.globals.url}/api/TemporaryPassword/Answer`, obj, this.httpOptions)
    //     .subscribe(
    //       (data: any) => {
    //         if (data.errorCode === undefined) {
    //           if (data.length !== 0) {
    //             sessionStorage.setItem('id', this.userData.id);
    //             sessionStorage.setItem('username', this.userData.profile.login);
    //             sessionStorage.setItem('email', this.userData.profile.email);
    //             sessionStorage.setItem('firstName', this.userData.profile.firstName);
    //             sessionStorage.setItem('lastName', this.userData.profile.lastName);
    //             sessionStorage.setItem('ibisid', this.userData.profile.ibisid);
    //             sessionStorage.setItem('driversLicense', this.userData.profile.drivers_license);
    //             sessionStorage.setItem('streetAddress', this.userData.profile.streetAddress);
    //             sessionStorage.setItem('city', this.userData.profile.city);
    //             sessionStorage.setItem('state', this.userData.profile.state);
    //             sessionStorage.setItem('zipCode', this.userData.profile.zipCode);
    //             sessionStorage.setItem('primaryPhone', this.userData.profile.primaryPhone);
    //             sessionStorage.setItem('status', this.userData.status);
    //             sessionStorage.setItem('birthDate', this.userData.birthDate);
    //             this.tempPassword = data;

    //             // this.closeButton.nativeElement.click();
    //             // this.route.navigate(['/view']);
    //           } else this.verifyValid = false;
    //         } else this.toastr.error('Please try again later', 'An error has occured!');
    //       },
    //       err => this.toastr.error('Please try again later', 'An error has occured!')
    //     )
    // }
  }

  // selectChangeHandler(evt: any) {
  //   if (evt.target.value === "dl") {
  //     this.display = "dl";
  //   } else {
  //     this.display = "sq";
  //   }
  // }
}