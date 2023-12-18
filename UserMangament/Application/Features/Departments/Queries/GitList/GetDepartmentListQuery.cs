using Application.Features.Departments.Dtos.GetList;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Queries.GitList
{
    public class GetDepartmentListQuery:IRequest<BaseCommandResponse<List<GetListDepartmentOutput>>>
    {
    }
}
