import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { OAuthParseService } from './services/oauth-parse-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private oauthService: OAuthService,
    private parseService: OAuthParseService) {
  }

  title = 'ProjectCoffeeWebUINg';
  userName = this.parseService.getEmail();

  public get isLoggedIn() {
    if(this.oauthService.getAccessToken() == null) return false;
    return true;
  }

  public logIn() {
    this.oauthService.initLoginFlow();
  }

  public logOut() {
    this.oauthService.logOut();
  }
}
