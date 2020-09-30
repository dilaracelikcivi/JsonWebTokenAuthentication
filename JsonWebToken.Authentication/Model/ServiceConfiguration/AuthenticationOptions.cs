using Microsoft.AspNetCore.Authentication.Cookies;

namespace JsonWebToken.Authentication.Model.ServiceConfiguration
{
    public class AuthenticationOptions
    {
        public string DefaultAuthenticateScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultChallengeScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultSignInScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultSignOutScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
    }
}