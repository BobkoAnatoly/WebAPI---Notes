using Microsoft.AspNetCore.Mvc;
using Application.BusinessLogic.Services.Interfaces;
using Application.Model.ModelsDto;
using Application.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register( RegisterDto userDto)
        {
            if (_userService.UserExists(userDto.Login))
                return BadRequest("UserName Is Already Taken");
            if (!_userService.Register(userDto)) return BadRequest();
            return Ok("user was registered");
        }
    }
}
