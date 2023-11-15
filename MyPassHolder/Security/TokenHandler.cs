using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyPassHolder.Security
{
    public static class TokenHandler
    {
        public static Token createToken(IConfiguration configuration, double? expiration, bool isAdmin = false, string email = "")        
        { 
            Token token = new();
            List<Claim> claims = new List<Claim>();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            if (expiration == null)             
                token.Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Token:Expiration"]));
            else            
                token.Expiration = DateTime.Now.AddMinutes((double)expiration);

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Name, "admin"));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            if (!email.IsNullOrEmpty())
            {
                claims.Add(new Claim(ClaimTypes.Email, email));
            }

            JwtSecurityToken jwtSecurityToken = new(
                //issuer: configuration["Token:Issuer"],
                //audience: configuration["Token:Audience"],
                claims:claims,
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            byte[] numbers = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            token.RefreshToken = Convert.ToBase64String(numbers);

            return token;
        }

        public static string decodeTokenForEmail(IConfiguration configuration, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.ReadJwtToken(token);
            //var emailClaim = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email);
            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            var email = emailClaim.Value;

            return email;
        }
    }
}
