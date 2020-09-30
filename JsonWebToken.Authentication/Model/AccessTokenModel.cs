using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace JsonWebToken.Authentication.Model
{
    public class AccessTokenModel
    {
        public AccessTokenModel()
        {
            
        }
        public AccessTokenModel(string secret, IEnumerable<Claim> userClaims)
        {
            Secret = secret;
            UserClaims = userClaims;
        }
        
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        [Required]
        public string Secret { get; set; }

        [Required]
        public IEnumerable<Claim> UserClaims { get; set; }
        
        public int ExpirationTime { get; set; } = 120;
        public string Algorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
    }
}