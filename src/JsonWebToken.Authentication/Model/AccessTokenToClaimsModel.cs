using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace JsonWebToken.Authentication.Model
{
    public class AccessTokenToClaimsModel
    {
        [Required] public string Secret { get; set; }
        [Required] public IEnumerable<Claim> UserClaims { get; set; }
    }
}