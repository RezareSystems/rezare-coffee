import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
 
@Component({
    templateUrl: "./login.component.html"
})
export class LoginComponent {
 
    constructor(private oauthService: OAuthService) {
    }
 
    public login() {
        this.oauthService.initLoginFlow();
    }
 
    public logoff() {
        this.oauthService.logOut();
    }
 
    public get name() {
        let claims = this.oauthService.getIdentityClaims();
        console.log(claims);
        if (!claims) return null;
        return '';//claims.given_name;
    }
 
}