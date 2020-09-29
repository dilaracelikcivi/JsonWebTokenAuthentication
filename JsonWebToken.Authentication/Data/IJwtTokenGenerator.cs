using System.Collections.Generic;
using System.Security.Claims;
using JsonWebToken.Authentication.Model.Token;

namespace JsonWebToken.Authentication.Data
{
    public interface IJwtTokenGenerator
    {
        // Use this method for ASP.NET Core web application with cookie authentication.
        TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(IEnumerable<Claim> userClaims);

        // Use this method for ASP.NET Core Web API applications with cookieless authentication.
        string GenerateAccessToken(IEnumerable<Claim> userClaims, string issuer, string audience, string secret,
            string algorithm, int expirationTime);
    }
}