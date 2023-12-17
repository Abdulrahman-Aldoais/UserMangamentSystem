using Application.Repositories.DepartmentRepository;
using Domain.Resources;
using FluentValidation;

namespace Application.Features.Departments.Queries.Git
{
    public class GetDepartmentQueryHandlerValidation : AbstractValidator<GetDepartmentQuery>
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        public GetDepartmentQueryHandlerValidation(IDepartmentReadRepository departmentReadRepository)
        {

            _departmentReadRepository = departmentReadRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x)
                   .MustAsync(IdIsNotExists)
                   .WithMessage(SharedResourcesKeys.IsNotExist);
        }

        private async Task<bool> IdIsNotExists(GetDepartmentQuery e, CancellationToken token)
        {
            var result = await _departmentReadRepository.GetAsync(x => x.Id == e.Id);
            return result != null;
        }
    }
}
