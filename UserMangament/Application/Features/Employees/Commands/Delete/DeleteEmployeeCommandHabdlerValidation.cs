using Application.Features.Employees.Commands.Delete;
using Application.Repositories.EmployeeRepositoty;
using Application.Repositories.UserRepository;
using FluentValidation;

namespace Application.Features.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandHabdlerValidation : AbstractValidator<DeleteEmployeeCommand>
    {
        private readonly IEmployeeReadRepositoty _employeeReadRepositoty;
        public DeleteEmployeeCommandHabdlerValidation(IEmployeeReadRepositoty employeeReadRepositoty)
        {
            _employeeReadRepositoty = employeeReadRepositoty;

            RuleFor(x => x.Id)
                       .NotEmpty()
                       .NotNull();

            RuleFor(x => x)
               .MustAsync(IdIsNotExists)
               .WithMessage("لم يتم العثور على رقم هذا المستخدم في قاعدة البيانات");

        }

        private async Task<bool> IdIsNotExists(DeleteEmployeeCommand e, CancellationToken token)
        {
            var result = await _employeeReadRepositoty.GetAsync(x => x.Id == e.Id);
            return result != null;
        }
    }
}