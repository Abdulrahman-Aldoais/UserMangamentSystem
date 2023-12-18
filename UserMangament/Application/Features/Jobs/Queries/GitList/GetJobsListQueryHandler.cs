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
    public class GetJobsListQueryHandler : IRequestHandler<GetJobsListQuery, BaseCommandResponse<List<GetJobsListOutput>>>
    {

        public Task<BaseCommandResponse<List<GetJobsListOutput>>> Handle(GetJobsListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
