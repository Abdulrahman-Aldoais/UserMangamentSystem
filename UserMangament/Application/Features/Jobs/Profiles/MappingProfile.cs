using Application.Features.Jobs.Dtos.Get;
using Application.Features.Jobs.Dtos.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Job , GetJobsOutput>().ReverseMap();
            CreateMap<Job , GetJobsListOutput>().ReverseMap();
        }
    }
}
