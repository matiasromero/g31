using System;
using System.Security.Cryptography;

namespace HomeSwitchHome.API.Infrastructure
{
    // <summary>
    //     JWT Token generator class using "secret-key"
    //     more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    // </summary>
    public interface ITokenGenerator
    {
        string GenerateRefreshToken();
    }

    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}