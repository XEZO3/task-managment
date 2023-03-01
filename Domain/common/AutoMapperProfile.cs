using AutoMapper;
using Domain.Models;
using Domain.Models.Dto;
using Domain.Models.ServiceRespone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.common
{
    public class AutoMapperProfile : Profile
    { 
        public AutoMapperProfile()
        {
            
            CreateMap<Users, UsersRespone>();
            CreateMap<RegisterDto, Users>();
            CreateMap<LoginDto, Users>();
            CreateMap<Tasks, TasksRepone>();
        }
    }
}
