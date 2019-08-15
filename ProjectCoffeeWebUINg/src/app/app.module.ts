import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import { NullValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { OAuthModule } from 'angular-oauth2-oidc';
import { CoffeeSelectComponent } from './coffee-select/coffee-select.component';
import { CoffeeListComponent } from './coffee-list/coffee-list.component';
import { ApiService } from './services/api-service';
import { OAuthParseService } from './services/oauth-parse-service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    CoffeeSelectComponent,
    CoffeeListComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    OAuthModule.forRoot({
      resourceServer: {
          allowedUrls: ['https://i1h2ug2l87.execute-api.ap-southeast-2.amazonaws.com/zombies'],
          sendAccessToken: true
      }
  })
  ],
  providers: [
    ApiService,
    OAuthParseService
  ],
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
