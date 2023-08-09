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

    public AccountController(IJwtTokenGenerator jwtTokenGenerator) => _jwtTokenGenerator = jwtTokenGenerator;
    
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
          ...
          
          // create claims
          var userClaims = new[]
          { 
               new Claim(ClaimTypes.Name, fullName),
               new Claim(ClaimTypes.Email, email)
          };
          
          // generate token
          var accessToken = _jwtTokenGenerator.GenerateAccessToken(new AccessTokenModel(yourSecretKey, userClaims));
          return Ok(accessToken);
    }
}
```

#### Usage in an Web Controller
```
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AccountController(IAccountService accountService, IJwtTokenGenerator jwtTokenGenerator)
    { 
          _accountService = accountService;
          _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
          // get generated api access token for user claims
          var apiAccessToken = await _accountService.Login(model);

          // decode incoming access token from api
          var decodedAccessToken = new JwtSecurityTokenHandler().ReadJwtToken(apiAccessToken);
    
          // create token model
          var accessTokenModel = new AccessTokenToClaimsModel
          {
               Secret = yourSecretKey,
               UserClaims = decodedAccessToken.userClaims
          };

          // generate new token by cookie based authentication
          var accessToken = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(accessTokenModel);
          
          // sign in the system
          await HttpContext.SignInAsync(accessToken.ClaimsPrincipal, accessToken.AuthenticationProperties);
          ...
    }
}
```

## Lets Check Content of Your Token

You can paste your generated token into the encoded side of the [JWT](https://jwt.io/#debugger-io?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c) website.
