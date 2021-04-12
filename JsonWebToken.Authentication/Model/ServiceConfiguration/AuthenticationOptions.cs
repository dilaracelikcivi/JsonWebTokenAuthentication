using Microsoft.AspNetCore.Authentication.Cookies;

namespace JsonWebToken.Authentication.Model.ServiceConfiguration
{
    public class AuthenticationOptions
    {
        public AuthenticationOptions()
        {
        }
        
        public AuthenticationOptions(string defaultAuthenticateScheme, string defaultChallengeScheme, 
            string defaultSignInScheme, string defaultSignOutScheme)
        {
            DefaultAuthenticateScheme = defaultAuthenticateScheme;
            DefaultChallengeScheme = defaultChallengeScheme;
            DefaultSignInScheme = defaultSignInScheme;
            DefaultSignOutScheme = defaultSignOutScheme;
        }
        
        public string DefaultAuthenticateScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultChallengeScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultSignInScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
        public string DefaultSignOutScheme { get; set; } = CookieAuthenticationDefaults.AuthenticationScheme;
    }
}