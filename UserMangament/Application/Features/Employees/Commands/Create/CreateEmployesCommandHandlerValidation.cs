using Application.Repositories.EmployeeRepositoty;
using Domain.Resources;
using FluentValidation;

namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployesCommandHandlerValidation : AbstractValidator<CreateEmployesCommand>
    {
        private readonly IEmployeeReadRepositoty _employeeReadRepositoty;
        public CreateEmployesCommandHandlerValidation(IEmployeeReadRepositoty employeeReadRepositoty)
        {
            _employeeReadRepositoty = employeeReadRepositoty;

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                 .NotNull().WithMessage(SharedResourcesKeys.Required)
                 .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthis100)
                 .MinimumLength(50).WithMessage("الاسم المدخل صغير ");


            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
              .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthis100);

            RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(x => x.Phone)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(9).WithMessage("قم بكتابة رقم الهاتف بالطريقة الصحيحة 9 ارقام فقط")
            .MinimumLength(9);

            RuleFor(x => x.JobId)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Salary)
            .InclusiveBetween(200000, 700000)
            .WithMessage("  يجب ان تكون قيمة الراتب بين  200 الف و 700 الف ك حد اقصى");

            RuleFor(x => x.JobTitle)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(x => x.WorkingHourId)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(x => x)
                 .MustAsync(NameCanNotBeDuplicatedWhenInserted)
                 .WithMessage("اسم الموظف موجود");




        }


        private async Task<bool> NameCanNotBeDuplicatedWhenInserted(CreateEmployesCommand e, CancellationToken token)
        {
            var result = await _employeeReadRepositoty.GetAsync(x => x.Name == e.Name);
            return result == null;
        }

    }
}
