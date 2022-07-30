using Application.BusinessLogic.Services.Interfaces;
using Application.Model.Database;
using Application.Model.Models;
using Application.Model.ModelsDto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationDatabaseContext context,
            IMapper mapper,
            ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public UserDto Register(RegisterDto registerDto)
        {
            try
            {
                var hmac = new HMACSHA512();
                var user = _mapper.Map<RegisterDto, User>(registerDto);
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
                user.PasswordSalt = hmac.Key;
                _context.Users.Add(user);
                _context.SaveChanges();

                var userDto = _mapper.Map<User, UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user);
                return userDto;
            }
            catch (Exception)
            {
                return null!;
            }
            
        }
        public UserDto Login(LoginDto loginDto)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Login == loginDto.Login);

            if (user == null) return null!;

            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return null!;
            }
            var userDto = _mapper.Map<User, UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return userDto;
        }
        public bool UserExists(string login)
        {
            return _context.Users
                .AsNoTracking()
                .Any(x => x.Login == login.ToLower()) ? true:false;
        }
    }
}
