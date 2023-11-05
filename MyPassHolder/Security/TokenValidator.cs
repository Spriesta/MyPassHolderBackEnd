using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyPassHolder.Security
{
    public static class TokenValidator
    {
        public static bool IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahmetkeskinmetin2irregularkin"));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = securityKey
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);  
                                                                                                                                              
                if (validatedToken.ValidTo < DateTime.UtcNow)              
                    return false; 
                
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
