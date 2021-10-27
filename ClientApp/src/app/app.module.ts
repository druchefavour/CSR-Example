import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NgxMaskModule, IConfig } from 'ngx-mask';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AuthGuard } from './shared';
import { environment } from 'src/environments/environment';

import { ToastrModule } from 'ngx-toastr';
import { Globals } from './globals';
import { VerifyComponent } from './verify/verify.component';
import { ViewComponent } from './view/view.component';

const config = {
  issuer: environment.issuer,
  redirectUri: environment.issuer,
  clientId: environment.clientId,
  scopes: ['openid', 'profile', 'email'],
  addUserUrl: environment.addUserUrl,
  pkce: true
}

const maskConfig: Partial<IConfig> = {
  validation: false
};

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    VerifyComponent,
    ViewComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    OAuthModule.forRoot(),
    NgxMaskModule.forRoot(maskConfig),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'view', component: ViewComponent, canActivate: [AuthGuard] }
    ])
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [Globals],
  bootstrap: [AppComponent]
})
export class AppModule { }
