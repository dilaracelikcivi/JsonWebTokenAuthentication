using System;
using JsonWebToken.Authentication.Data;
using Microsoft.Extensions.DependencyInjection;
using JsonWebToken.Authentication.Model.ServiceConfiguration;

namespace JsonWebToken.Authentication.Services
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, 
            AuthenticationOptions authenticationOptions = null, CookieOptions cookieOptions = null)
        {
            // initialize authentication options if it is null
            if (authenticationOptions == null) authenticationOptions = new AuthenticationOptions();
            
            // initialize cookie options if this parameter is null
            if (cookieOptions == null) cookieOptions = new CookieOptions();

            // configure authentication and cookie options with specified options
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = authenticationOptions.DefaultAuthenticateScheme;
                    options.DefaultChallengeScheme = authenticationOptions.DefaultChallengeScheme;
                    options.DefaultSignInScheme = authenticationOptions.DefaultSignInScheme;
                    options.DefaultSignOutScheme = authenticationOptions.DefaultSignOutScheme;
                })
                .AddCookie(options =>
                {
                    if (cookieOptions.ReturnUrlParameter != null)
                        options.ReturnUrlParameter = cookieOptions.ReturnUrlParameter;

                    if (cookieOptions.CookieName != null)
                        options.Cookie.Name = cookieOptions.CookieName;

                    if (cookieOptions.ExpireTime > 0)
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieOptions.ExpireTime);
                });
            
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
    }
}