import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class OAuthParseService {
    constructor(private oauthService: OAuthService) { }

    getSubject() : string {
        var claimsObject = this.oauthService.getIdentityClaims();
        var claims = JSON.parse(JSON.stringify(claimsObject));
        if(!claims) return null;
        return claims.sub;
    }    

    getEmail() : string {
        var claimsObject = this.oauthService.getIdentityClaims();
        var claims = JSON.parse(JSON.stringify(claimsObject));
        if(!claims) return null;
        return claims.email;
    }

    getName() : string {
        var claimsObject = this.oauthService.getIdentityClaims();
        var claims = JSON.parse(JSON.stringify(claimsObject));
        if(!claims) return null;
        return claims.name;
    }
}