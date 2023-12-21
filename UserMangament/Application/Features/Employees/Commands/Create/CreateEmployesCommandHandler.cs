using Application.Features.Employees.Dtos.Get;
using Application.Repositories.EmployeeRepositoty;
using Application.Services.EmployeeServices;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Domain.Resources;
using MediatR;

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployesCommandHandler : BaseCommandResponseHandler, IRequestHandler<CreateEmployesCommand, BaseCommandResponse<GetEmployeeOutput>>
    {
        private readonly IEmployeeReadRepositoty _employeeReadRepositoty;
        private readonly IEmployeeWriteRepositoty _employeeWriteRepositoty;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public CreateEmployesCommandHandler(IEmployeeReadRepositoty employeeReadRepositoty, IMapper mapper, IEmployeeWriteRepositoty employeeWriteRepositoty, IEmployeeService employeeService)
        {
            _employeeReadRepositoty = employeeReadRepositoty;
            _mapper = mapper;
            _employeeWriteRepositoty = employeeWriteRepositoty;
            _employeeService = employeeService;
        }
        public async Task<BaseCommandResponse<GetEmployeeOutput>> Handle(CreateEmployesCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetEmployeeOutput>();
            var validator = new CreateEmployesCommandHandlerValidation(_employeeReadRepositoty);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.IsValid)
            {
                response.Data = null;
                response.Success = false;
                //   response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = SharedResourcesKeys.validateAllExpectedFieldsReceivedInDatabase;
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var employeeMapp = _mapper.Map<Employee>(request);
                employeeMapp.CreatedDate = DateTime.Now;
                employeeMapp.IsActive = true;


                var insertEmployeeInToDatabase = await _employeeService.AddNewEmployee(employeeMapp);
                switch (insertEmployeeInToDatabase)
                {
                    case "Failed": return BadRequest<GetEmployeeOutput>(SharedResourcesKeys.TryToRegisterAgain);
                    case "Success": return Success(response.Data);
                    default: return BadRequest<GetEmployeeOutput>(SharedResourcesKeys.FaildToAddUser);
                }
            }
            return response;
        }
    }
}
