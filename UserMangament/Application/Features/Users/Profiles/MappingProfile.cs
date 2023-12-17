using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Dtos.Get;
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
            CreateMap<User, GetUserOutput>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<GetUserOutput, UpdateUserCommand>().ReverseMap();
            CreateMap<User, DeleteUserCommand>().ReverseMap();



        }
    }
}
