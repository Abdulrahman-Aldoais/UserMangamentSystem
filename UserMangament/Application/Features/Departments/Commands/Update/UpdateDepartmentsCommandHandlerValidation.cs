using Application.Repositories.DepartmentRepository;
using Domain.Resources;
using FluentValidation;

namespace Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentsCommandHandlerValidation : AbstractValidator<UpdateDepartmentsCommand>
    {
        public UpdateDepartmentsCommandHandlerValidation()
        {

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                 .NotNull().WithMessage(SharedResourcesKeys.Required)
                 .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthis100)
                 .MinimumLength(50).WithMessage("الاسم المدخل صغير ");
        }

    }
}
