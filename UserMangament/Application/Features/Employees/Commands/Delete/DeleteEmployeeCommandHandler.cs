using Application.Features.Employees.Dtos.Get;
using Application.Repositories.EmployeeRepositoty;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;


namespace Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, BaseCommandResponse<int>>
    {
        private readonly IEmployeeWriteRepositoty _employeeWriteRepositoty;
        private readonly IEmployeeReadRepositoty _employeesReadRepositoty;
        private readonly IMapper _mapper;
        public DeleteEmployeeCommandHandler(IEmployeeWriteRepositoty employeeWriteRepositoty,IEmployeeReadRepositoty employeeReadRepositoty,IMapper mapper)
        {
            _employeeWriteRepositoty = employeeWriteRepositoty;
            _employeesReadRepositoty = employeeReadRepositoty;
            _mapper = mapper;   
        }
        public async Task<BaseCommandResponse<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<int>();
            var validator = new DeleteEmployeeCommandHabdlerValidation(_employeesReadRepositoty);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Data = request.Id;
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = null;
                response.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {

                var getEmployeeFromDataBase = await _employeesReadRepositoty.GetAsync(x => x.Id.Equals(request.Id));
                await _employeeWriteRepositoty.DeleteAsync(getEmployeeFromDataBase);
                var userMapp = _mapper.Map<GetEmployeeOutput>(getEmployeeFromDataBase);

                response.Id = userMapp.Id;
                response.Data = userMapp.Id;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Deleted;
                response.Errors = null;

            }
            return response;
        }
    }
}
