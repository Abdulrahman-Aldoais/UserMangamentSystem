using Application.Features.Departments.Dtos.Get;
using Application.Features.Employees.Commands.Create;
using Application.Repositories.DepartmentRepository;
using Application.Services.DepartmentService;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Domain.Resources;
using MediatR;

namespace Application.Features.Departments.Commands.Create
{
    public class UpdateDepartmentsCommandHandler : BaseCommandResponseHandler, IRequestHandler<CreateDepartmentsCommand, BaseCommandResponse<GetDepartmentOutput>>
    {
        private readonly IDepartmentReadRepository _departmentReadRepositoty;
        private readonly IDepartmentWriteRepository _departmentWriteRepositoty;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public UpdateDepartmentsCommandHandler(IDepartmentReadRepository departmentReadRepositoty, IMapper mapper, IDepartmentWriteRepository departmentWriteRepositoty, IDepartmentService departmentService)
        {
            _departmentReadRepositoty = departmentReadRepositoty;
            _mapper = mapper;
            _departmentWriteRepositoty = departmentWriteRepositoty;
            _departmentService = departmentService;
        }
        public async Task<BaseCommandResponse<GetDepartmentOutput>> Handle(CreateDepartmentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetDepartmentOutput>();
            var validator = new CreateDepartmentsCommandHandlerValidation(_departmentReadRepositoty);
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
                var departmentMapp = _mapper.Map<Department>(request);
                departmentMapp.CreatedDate = DateTime.Now;


                var insertDepartmentInToDatabase = await _departmentService.AddNewDepartment(departmentMapp);
                switch (insertDepartmentInToDatabase)
                {
                    case "Failed": return BadRequest<GetDepartmentOutput>(SharedResourcesKeys.TryToRegisterAgain);
                    case "Success": return Success(response.Data);
                    default: return BadRequest<GetDepartmentOutput>(SharedResourcesKeys.FaildToAddUser);
                }
            }
            return response;
        }
    }
}
