using Core.Application.Responses;
using MediatR;

namespace Application.Features.Employees.Queries.Git
{
    public class GetEmployeeQuery : IRequest<BaseCommandResponse<GetEmployeeQuery>>
    {
        public int Id { get; set; }
    }
}
