using Application.Features.Departments.Dtos.Get;
using Application.Repositories.DepartmentRepository;
using Application.Services.DepartmentService;
using Core.Application.Responses;
using MediatR;
using School.Domain.Resources;

namespace Application.Features.Departments.Queries.Git
{
    public class GetDepartmentQueryHandler : BaseCommandResponseHandler, IRequestHandler<GetDepartmentQuery, BaseCommandResponse<GetDepartmentOutput>>
    {
        private readonly IDepartmentReadRepository _readRepository;
        private readonly IDepartmentService _departmentService;
        public GetDepartmentQueryHandler(IDepartmentReadRepository departmentReadRepository, IDepartmentService departmentService)
        {
            _readRepository = departmentReadRepository;
            _departmentService = departmentService;
        }
        public async Task<BaseCommandResponse<GetDepartmentOutput>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var respons = new BaseCommandResponse<GetDepartmentOutput>();
            var validator = new GetDepartmentQueryHandlerValidation(_readRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.IsValid)
            {
                respons.Data = null;
                respons.Success = false;
                respons.Message = SharedResourcesKeys.IsNotExist;
                respons.StatusCode = System.Net.HttpStatusCode.NotFound;
                respons.Success = false;
                respons.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                var result = await _departmentService.GetDepartmentByIdAsync(request.Id);

                respons.Id = result.Id;
                respons.Data = result;
                respons.Success = true;
                respons.StatusCode = System.Net.HttpStatusCode.OK;
                respons.Message = SharedResourcesKeys.Success;
                respons.Errors = null;
            }
            return respons;
        }
    }
}
