using Application.Features.Departments.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentsCommand : IRequest<BaseCommandResponse<GetDepartmentOutput>>
    {
        public string Name { get; set; }
        public int? CreatedBy { get; set; }
    }
}
