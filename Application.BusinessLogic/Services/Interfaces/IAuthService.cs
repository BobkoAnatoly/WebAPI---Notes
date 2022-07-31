
using Application.Model.Models;
using Application.Model.ModelsDto;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface IAuthService
    {
        TokenDto Token(LoginDto loginDto);
        TokenDto GetToken(User user);
        TokenDto Refresh(RefreshTokenDto model);

    }
}
