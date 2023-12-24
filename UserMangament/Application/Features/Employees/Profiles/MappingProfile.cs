using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Dtos.Get;
using Application.Features.Employees.Dtos.GetList;
using Application.Features.Users.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, GetEmployeeOutput>().ReverseMap();
            CreateMap<Employee, GetEmployeeListOutput>().ReverseMap();
            CreateMap < Employee , CreateEmployesCommand>().ReverseMap();
        }
    }
}
