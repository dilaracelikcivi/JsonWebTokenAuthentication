
namespace JsonWebToken.Authentication.Model.ServiceConfiguration
{
    public class CookieOptions
    {
        public string ReturnUrlParameter { get; set; }
        public string CookieName { get; set; }
        public int ExpireTime { get; set; }
    }
}