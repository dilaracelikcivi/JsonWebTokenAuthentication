# JSON Web Token for ASP .Net Core 

> The JSONWebToken.Authentication structure that enables an application to use token based authentication for API and cookie based authentication for browser based web applications.

## Installation 

Install [JsonWebToken.Authentication](https://www.nuget.org/packages/JsonWebToken.Authentication) package now, if you want to try. 

## Package Manager Console

```
Install-Package JsonWebToken.Authentication -Version 1.1.0
```

### .NET CLI

```
dotnet add package JsonWebToken.Authentication --version 1.1.0
```

## Usage
```
public void ConfigureServices(IServiceCollection services)
{
     services.AddControllers();
     services.AddJwtAuthentication(new AuthenticationOptions(), new CookieOptions());
}
```
