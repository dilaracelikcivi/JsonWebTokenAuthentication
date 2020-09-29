using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using JsonWebToken.Authentication.Model.Token;
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
        /// <param name="userClaims"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AuthenticationException"></exception>
        public TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(IEnumerable<Claim> userClaims)
        {
            // check claim is null or not
            if (userClaims == null)
                throw new ArgumentException("User claim cannot be null !");

            // convert claim type as list
            userClaims = userClaims.ToList();

            // generate access token
            var token = GenerateAccessToken(userClaims);

            // check that token generated or not
            if (token == null)
                throw new AuthenticationException("Generate token failed !");

            // create user identity, principal and properties
            var userIdentity = new ClaimsIdentity(userClaims, "Login");
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
        /// <param name="userClaims"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <param name="algorithm"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public string GenerateAccessToken(IEnumerable<Claim> userClaims, string issuer = null, string audience = null,
            string secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==",
            string algorithm = SecurityAlgorithms.HmacSha256Signature, int expirationTime = 120)
        {
            // create a security key
            var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(secret));

            // create a sign in credential
            var signinCredential = new SigningCredentials(secretKey, algorithm);

            // generate jwt access token
            var token = new JwtSecurityToken(issuer, audience, userClaims, DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expirationTime), signinCredential);

            // write generated token 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        ///     This method provides to create an authentication properties
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private AuthenticationProperties CreateAuthenticationProperties(string token)
        {
            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(
                new[]
                {
                    new AuthenticationToken
                    {
                        Name = "jwt",
                        Value = token
                    }
                }
            );

            return authenticationProperties;
        }
    }
}