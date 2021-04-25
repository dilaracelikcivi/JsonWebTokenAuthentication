# JSON Web Token for ASP .Net Core 

> The JSONWebToken.Authentication structure that enables an application to use token based authentication for API and cookie based authentication for browser based web applications.

## Installation 

Install [JsonWebToken.Authentication](https://www.nuget.org/packages/JsonWebToken.Authentication) package now, if you want to try it. 

## Package Manager Console

```
Install-Package JsonWebToken.Authentication -Version 1.1.0
```

### .NET CLI

```
dotnet add package JsonWebToken.Authentication --version 1.1.0
```

## Usage

#### Startup

```
public void ConfigureServices(IServiceCollection services)
{
     services.AddJwtAuthentication();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
       app.UseAuthentication();
}
```

#### Usage in an Api Controller
```
public class AccountController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AccountController(IJwtTokenGenerator jwtTokenGenerator)
    {
         _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
          ...
          var accessToken = _jwtTokenGenerator.GenerateAccessToken(new AccessTokenModel(yourSecretKey, userClaims));
          ...
    }
}
```

#### Usage in an Web Controller
```
public class AccountController : Controller
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AccountController(IJwtTokenGenerator jwtTokenGenerator)
    {
         _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
          ...
          var accessTokenModel = new AccessTokenToClaimsModel
          {
               Secret = yourSecretKey,
               UserClaims = userClaims
          };

          var accessToken = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(accessTokenModel);
          
          await HttpContext.SignInAsync(accessToken.ClaimsPrincipal, accessToken.AuthenticationProperties);
          ...
    }
}
```
