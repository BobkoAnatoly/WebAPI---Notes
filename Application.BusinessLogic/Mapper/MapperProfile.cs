using Application.Model.Models;
using Application.Model.ModelsDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
