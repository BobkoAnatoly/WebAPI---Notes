using Application.Model.Models;
using Application.Model.ModelsDto;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public bool Register(RegisterDto registerDto);
        public bool UserExists(string login);
    }
}
