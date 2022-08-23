using Application.BusinessLogic.Helpers.Cryptography;
using Application.BusinessLogic.Services.Interfaces;
using Application.Model.Database;
using Application.Model.Models;
using Application.Model.ModelsDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDatabaseContext context,
            ITokenService tokenService,
            IMapper mapper)
        {

            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public TokenDto GetToken(User user)
        {
            var token = _tokenService.CreateToken(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = tokenHandler.WriteToken(token);
            var refreshToken = AddRefreshToken(user);
            user.RefreshToken = refreshToken;
            var mappedUser = _mapper.Map<User, UserDto>(user);
            return new TokenDto
            {
                UserId = user.Id,
                Token = encodedJwt,
                RefreshToken = refreshToken.Id,
                Username = user.Login,
                User = mappedUser,
                ValidFrom = token.ValidFrom,
                ValidTo = token.ValidTo
            };
        }

        //сохранение рефреш токена в бд
        private RefreshToken AddRefreshToken(User user)
        {
            var now = DateTime.UtcNow;
            var token = new RefreshToken
            {
                Id = new JwtSecurityTokenHandler().WriteToken(_tokenService.CreateToken(user)),
                UserId = user.Id,
                ValidTo = now.AddDays(1),
                ValidFrom = now
            };
            _context.RefreshTokens.Add(token);
            _context.SaveChanges();
            return token;
        }

        public TokenDto Token(LoginDto loginDto)
        {
            var user = AuthUser(loginDto);
            return GetToken(user);
        }
        //аутентификация
        public User AuthUser(LoginDto loginDto)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Login == loginDto.Login);

            if (user == null) throw new Exception("Пользователь не найден.");

            if (PasswordHasher.VerifyPassword(
                user.PasswordSalt,
                user.PasswordHash,
                loginDto.Password))
            {
                return user;
            }
            throw new Exception("Неверный пароль.");
        }

        public TokenDto Refresh(RefreshTokenDto model)
        {
            var refreshToken = _context.RefreshTokens
                .FirstOrDefault(x => x.Id == model.RefreshToken);

            if (refreshToken == null) throw new Exception("refreshIsExpired");
            if (refreshToken.ValidTo < DateTime.UtcNow) throw new Exception("refreshIsExpired");

            var user = _context.Users.FirstOrDefault(x => x.Login == model.Login);
            if (user == null) throw new Exception("Пользователь не найден");
            _context.RefreshTokens.Remove(refreshToken);
            _context.SaveChanges();
            return GetToken(user);
        }
    }

}
