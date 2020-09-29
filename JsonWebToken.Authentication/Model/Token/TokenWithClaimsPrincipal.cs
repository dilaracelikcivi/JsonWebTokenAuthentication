using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace JsonWebToken.Authentication.Model.Token
{
    public class TokenWithClaimsPrincipal
    {
        public string AccessToken { get; internal set; }
        public ClaimsPrincipal ClaimsPrincipal { get; internal set; }
        public AuthenticationProperties AuthenticationProperties { get; internal set; }
    }
}