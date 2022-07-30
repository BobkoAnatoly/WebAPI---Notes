using Microsoft.AspNetCore.Mvc;
using Application.BusinessLogic.Services.Interfaces;
using Application.Model.ModelsDto;
using Application.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public ActionResult<UserDto> Register([FromBody] RegisterDto userDto)
        {
            if (_userService.UserExists(userDto.Login))
                return BadRequest("UserName Is Already Taken");
            var user = _userService.Register(userDto);
            if (user == null) return BadRequest();
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<UserDto> Login(LoginDto loginDto)
        {
            var user = _userService.Login(loginDto);
            if (user == null) 
                return Unauthorized("Invalid Login or Password");
            return Ok(user);
        }
        [HttpGet, Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
