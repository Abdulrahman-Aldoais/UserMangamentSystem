using Application.Features.Departments.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Departments.Queries.Git
{
    public class GetDepartmentQuery : IRequest<BaseCommandResponse<GetDepartmentOutput>>
    {
        public int Id { get; set; }
    }
}
