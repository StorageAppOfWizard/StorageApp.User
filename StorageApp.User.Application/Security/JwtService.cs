using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StorageApp.User.Application.Contracts;
using StorageApp.User.Domain.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StorageApp.User.Application.Security
{
    public class JwtService : IJwtService
    {
        private int MAX_HOURS_EXPIRE_TOKEN = 4;
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserModel user)
        {
            var handler = new JwtSecurityTokenHandler();
            var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(MAX_HOURS_EXPIRE_TOKEN),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public static ClaimsIdentity GenerateClaims(UserModel user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in user.Role)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }

            return ci;
        }
    }
}
