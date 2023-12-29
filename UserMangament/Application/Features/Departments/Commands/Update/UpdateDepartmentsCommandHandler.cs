using Application.Features.Departments.Dtos.Get;
using Application.Repositories.DepartmentRepository;
using Application.Services.DepartmentService;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentsCommandHandler : BaseCommandResponseHandler, IRequestHandler<UpdateDepartmentsCommand, BaseCommandResponse<GetDepartmentOutput>>
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
        public async Task<BaseCommandResponse<GetDepartmentOutput>> Handle(UpdateDepartmentsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetDepartmentOutput>();
            var validator = new UpdateDepartmentsCommandHandlerValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Data = null;
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Message = SharedResourcesKeys.validateAllExpectedFieldsReceivedInDatabase;
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var getDepartmentById = await _departmentReadRepositoty.GetAsync(x => x.Id == request.Id);

                getDepartmentById.Id = request.Id;
                getDepartmentById.Name = request.Name;
                getDepartmentById.ModifiedDate = DateTime.Now;
                // getDepartmentById.AccountCancellationStatusBy = 1;
                getDepartmentById.ModifiedBy = 1;

                var createResult = await _departmentService.UpdateDepartment(getDepartmentById);

                var departmentMapp = _mapper.Map<GetDepartmentOutput>(request);
                response.Id = departmentMapp.Id;
                response.Data = departmentMapp;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = SharedResourcesKeys.Success;
                switch (createResult)
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
