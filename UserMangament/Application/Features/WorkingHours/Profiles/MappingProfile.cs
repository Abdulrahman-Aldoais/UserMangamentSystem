using Application.Features.WorkingHours.Dtos.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.WorkingHours.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkingHour, GetListWorkingHourOutput>().ReverseMap();
        }
    }
}
