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
                 .MaximumLength(50).WithMessage(SharedResourcesKeys.MaxLengthis100)
                 .MinimumLength(10).WithMessage("الاسم المدخل صغير ");
        }

    }
}
