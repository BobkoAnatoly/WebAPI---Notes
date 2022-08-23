using Application.Model.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken CreateToken(User user);
    }
}
