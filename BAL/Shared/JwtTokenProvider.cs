using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MODEL.Entities;
using System.Security.Claims;
using System.Text;

namespace BAL.Shared
{
    public class JwtTokenProvider(IConfiguration configuration)
    {
        public string Create (Users user, string role)
        {
            string secretKey = configuration["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role) // Add role claim
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpirationInMinutes"])),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
