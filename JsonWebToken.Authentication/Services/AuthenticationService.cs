using System;
using JsonWebToken.Authentication.Data;
using JsonWebToken.Authentication.Model.CookieConfiguration;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationOptions = JsonWebToken.Authentication.Model.AuthenticationConfiguration.AuthenticationOptions;

namespace JsonWebToken.Authentication.
    Services
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            AuthenticationOptions authenticationOptions, CookieOptions cookieOptions)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
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

            return services;
        }
    }
}