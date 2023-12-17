using Application.Features.Employees.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployesCommandHandler : IRequestHandler<CreateEmployesCommand, BaseCommandResponse<GetEmployeeOutput>>
    {
        public Task<BaseCommandResponse<GetEmployeeOutput>> Handle(CreateEmployesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
