using JsonWebToken.Authentication.Data;
using JsonWebToken.Authentication.Model;
using NUnit.Framework;

namespace test.JsonWebToken.Authentication
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateAccessTokenWithClaimsPrincipal()
        { 
            IJwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator();
            var result = jwtTokenGenerator.GenerateAccessToken(new AccessTokenModel());
            
            Assert.Pass();
        }
    }
}