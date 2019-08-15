import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private oauthService: OAuthService) {
  }

  title = 'ProjectCoffeeWebUINg';

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
