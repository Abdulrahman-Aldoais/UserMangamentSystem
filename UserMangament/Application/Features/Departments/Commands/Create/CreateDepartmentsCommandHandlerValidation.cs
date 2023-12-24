using Application.Repositories.DepartmentRepository;
using Application.Repositories.EmployeeRepositoty;
using Domain.Resources;
using FluentValidation;

namespace Application.Features.Departments.Commands.Create
{
    public class CreateDepartmentsCommandHandlerValidation : AbstractValidator<CreateDepartmentsCommand>
    {
        private readonly IDepartmentReadRepository _depatmentReadRepositoty;
        public CreateDepartmentsCommandHandlerValidation(IDepartmentReadRepository depatmentReadRepositoty)
        {
            _depatmentReadRepositoty = depatmentReadRepositoty;

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                 .NotNull().WithMessage(SharedResourcesKeys.Required)
                 .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthis100)
                 .MinimumLength(50).WithMessage("الاسم المدخل صغير ");

            RuleFor(x => x)
                 .MustAsync(NameCanNotBeDuplicatedWhenInserted)
                 .WithMessage("اسم القسم موجود");




        }


        private async Task<bool> NameCanNotBeDuplicatedWhenInserted(CreateDepartmentsCommand e, CancellationToken token)
        {
            var result = await _depatmentReadRepositoty.GetAsync(x => x.Name == e.Name);
            return result == null;
        }

    }
}
