using Application.Model.Models;
using Application.Model.ModelsDto;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public UserDto Register(RegisterDto registerDto);
        public UserDto Login(LoginDto loginDto);
        public bool UserExists(string login);
    }
}
