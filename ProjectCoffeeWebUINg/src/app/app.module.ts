import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import { NullValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { OAuthModule } from 'angular-oauth2-oidc';
import { LoginComponent } from './login/login.component';
import { CoffeeSelectComponent } from './coffee-select/coffee-select.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CoffeeSelectComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    OAuthModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private oauthService: OAuthService) {
    this.configure();
  }

  private configure() {
    this.oauthService.configure(authConfig);
    this.oauthService.tokenValidationHandler = new NullValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndLogin();
    this.oauthService.tryLogin({
      onTokenReceived: context => {
          //
          // Output just for purpose of demonstration
          // Don't try this at home ... ;-)
          //
          console.debug("logged in");
          console.debug(context);
      }
  });
  }
}
