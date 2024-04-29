using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loginsystem.Dto;
using Loginsystem.Models;

namespace Loginsystem.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreationDto,User>();
            CreateMap<User,UserCreationDto>();
        }
    }
}