using Application.Features.Employees.Dtos.GetList;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GitList
{
    public class GetEmployeeListQuery :IRequest<BaseCommandResponse<List<GetEmployeeListOutput>>>
    {
    }
}
