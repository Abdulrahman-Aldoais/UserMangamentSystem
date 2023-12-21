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
        }
    }
}
