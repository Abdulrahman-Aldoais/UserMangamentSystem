using Application.Features.Jobs.Dtos.GetList;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Jobs.Queries.GitList
{
    public class GetJobsListQuery:IRequest<BaseCommandResponse<List<GetJobsListOutput>>>
    {

    }
}
