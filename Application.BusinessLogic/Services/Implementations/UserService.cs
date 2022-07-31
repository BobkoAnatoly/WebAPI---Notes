using Application.BusinessLogic.Helpers.Cryptography;
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

        public bool Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<RegisterDto, User>(registerDto);
            PasswordHasher.HashPassword(registerDto.Password, out byte[] passwordHash,
                out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            _context.Users.Add(user);
            return _context.SaveChanges() > 0 ? true : false;

        }
        
        public bool UserExists(string login)
        {
            return _context.Users
                .AsNoTracking()
                .Any(x => x.Login == login.ToLower()) ? true : false;
        }
    }
}
