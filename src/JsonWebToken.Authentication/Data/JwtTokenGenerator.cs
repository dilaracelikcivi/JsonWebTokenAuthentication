using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using JsonWebToken.Authentication.Extensions;
using JsonWebToken.Authentication.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebToken.Authentication.Data
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        /// <summary>
        /// This method provides to generate a JSON Web Token with a ClaimsPrincipal object both containing the claims passed in.
        /// Use this method for ASP.NET Core web application with cookie authentication.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AuthenticationException"></exception>
        public TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(AccessTokenToClaimsModel model)
        {
            // validate model
            if (!model.IsValid()) throw new ArgumentException(model.ValidationResult());

            // generate access token
            var token = GenerateAccessToken(new AccessTokenModel(model.Secret, model.UserClaims));

            // check that token is generated or not
            if (token == null) throw new AuthenticationException("Token could not generate!");

            // create user identity, principal and properties
            var userIdentity = new ClaimsIdentity(model.UserClaims, "Login");
            var principal = new ClaimsPrincipal(userIdentity);
            var properties = CreateAuthenticationProperties(token);

            return new TokenWithClaimsPrincipal
            {
                AccessToken = token,
                ClaimsPrincipal = principal,
                AuthenticationProperties = properties
            };
        }

        /// <summary>
        /// This method provides to generate a string JSON Web Token containing the claims passed in.
        /// Use this method for ASP.NET Core Web API applications with cookieless authentication.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GenerateAccessToken(AccessTokenModel model)
        {
            // validate model
            if (!model.IsValid()) throw new ArgumentNullException(model.ValidationResult());

            // create a security key
            var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(model.Secret));

            // create a sign in credential
            var signinCredential = new SigningCredentials(secretKey, model.Algorithm);

            // generate jwt access token
            var token = new JwtSecurityToken(model.Issuer, model.Audience, model.UserClaims, DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(model.ExpirationTime), signinCredential);

            // write generated token 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// This method provides to create an authentication properties
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private AuthenticationProperties CreateAuthenticationProperties(string token)
        {
            var authenticationProperties = new AuthenticationProperties();
            
            var storeTokenList = new[]
            {
                new AuthenticationToken
                {
                    Name = "jwt",
                    Value = token
                }
            };

            authenticationProperties.StoreTokens(storeTokenList);
            return authenticationProperties;
        }
    }
}