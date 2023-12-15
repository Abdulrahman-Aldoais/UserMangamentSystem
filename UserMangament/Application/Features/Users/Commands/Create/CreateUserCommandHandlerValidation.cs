using FluentValidation;
using School.Domain.Resources;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandlerValidation : AbstractValidator<CreateUserCommand>
    {

        public CreateUserCommandHandlerValidation()
        {

            RuleFor(x => x.UserName)
             .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
             .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
        }
    }
}
