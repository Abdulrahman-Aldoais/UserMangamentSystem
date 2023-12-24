using Application.Features.Departments.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentsCommand : IRequest<BaseCommandResponse<GetDepartmentOutput>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
