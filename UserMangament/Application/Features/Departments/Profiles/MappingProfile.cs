using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Departments.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, GetListDepartmentOutput>().ReverseMap();
            CreateMap<Department, GetDepartmentOutput>().ReverseMap();
            CreateMap<Department, CreateDepartmentsCommand>().ReverseMap();
            CreateMap<Department, UpdateDepartmentsCommand>().ReverseMap();
            CreateMap<UpdateDepartmentsCommand, GetDepartmentOutput>();

        }
    }
}
