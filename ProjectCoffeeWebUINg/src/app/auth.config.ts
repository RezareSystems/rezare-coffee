import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: 'https://login.microsoftonline.com/6ab8b79b-42b6-40fd-a177-4540c8f1b365/v2.0',
  
  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/login',
  
  // The SPA's id. The SPA is registered with this id at the auth-server
  clientId: 'b5b2aa1e-66ba-40ff-a3f8-425cc6beaaee',
  
  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid profile email',

  strictDiscoveryDocumentValidation: false,
  responseType: 'id_token token'
}