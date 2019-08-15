using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace ProjectCoffeeAuthorizerLambda
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest input, ILambdaContext context)
        {
            var inputToken = input.AuthorizationToken;
            var ok = false;
            var subject = "<< NOT FOUND >>";

            context.Logger.Log($"Token: {inputToken}");

            if (!string.IsNullOrWhiteSpace(inputToken))
            {
                try
                {
                    var token = new JwtSecurityToken(inputToken);
                    ok = token.Issuer == "https://login.microsoftonline.com/6ab8b79b-42b6-40fd-a177-4540c8f1b365/v2.0";
                    context.Logger.Log($"Issuer: {token.Issuer}");
                    context.Logger.Log($"Issuer Valid: {ok}");
                    context.Logger.Log($"Subject: {token.Subject}");
                    subject = token.Subject;
                }
                catch (Exception e)
                {
                    context.Logger.Log($"Exception: {e.Message}");
                    context.Logger.Log(e.StackTrace);
                }
            }

            return new APIGatewayCustomAuthorizerResponse
            {
                PrincipalID = subject,//principal info here...
                UsageIdentifierKey = "API",//usage identifier here (optional)
                PolicyDocument = new APIGatewayCustomAuthorizerPolicy
                {
                    Version = "2012-10-17",
                    Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>() {
                        new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement
                        {
                            Action = new HashSet<string>(){"execute-api:Invoke"},
                            Effect = ok ? "Allow" : "Deny",
                            Resource = new HashSet<string>(){ input.MethodArn } // resource arn here
                        }
                    },
                }
            };
        }
    }
}
