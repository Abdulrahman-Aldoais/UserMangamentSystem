using Application.Repositories.UserRepository;
using FluentValidation;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHabdlerValidation : AbstractValidator<DeleteUserCommand>
    {
        private readonly IUserReadRepository _userReadRepository;
        public DeleteUserCommandHabdlerValidation(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;

            RuleFor(x => x.Id)
                       .NotEmpty()
                       .NotNull();

            RuleFor(x => x)
               .MustAsync(IdIsNotExists)
               .WithMessage("لم يتم العثور على رقم هذا المستخدم في قاعدة البيانات");

        }

        private async Task<bool> IdIsNotExists(DeleteUserCommand e, CancellationToken token)
        {
            var result = await _userReadRepository.GetAsync(x => x.Id == e.Id);
            return result != null;
        }
    }
}