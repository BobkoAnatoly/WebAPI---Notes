using Application.BusinessLogic.Services.Interfaces;
using Application.Model.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.BusinessLogic.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public JwtSecurityToken CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Login)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var Jwt = new JwtSecurityToken(
                issuer: "WebApi",
                audience:"WebApiUser",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds,
                notBefore: DateTime.UtcNow
                );

            return Jwt;
        }
    }
}
