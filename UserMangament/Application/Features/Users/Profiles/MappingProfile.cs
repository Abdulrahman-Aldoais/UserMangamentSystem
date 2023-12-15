using Application.Features.Users.Dtos.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetListUserOutput>().ReverseMap();
            CreateMap<User, GetListUserOutput>().ReverseMap();

        }
    }
}
