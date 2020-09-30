using JsonWebToken.Authentication.Model;

namespace JsonWebToken.Authentication.Data
{
    public interface IJwtTokenGenerator
    {
        // Use this method for ASP.NET Core web application with cookie authentication.
        TokenWithClaimsPrincipal GenerateAccessTokenWithClaimsPrincipal(AccessTokenToClaimsModel model);

        // Use this method for ASP.NET Core Web API applications with cookieless authentication.
        string GenerateAccessToken(AccessTokenModel model);
    }
}