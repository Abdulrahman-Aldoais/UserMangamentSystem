using Domain.Resources;
using FluentValidation;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateUserCommandHandlerValidation : AbstractValidator<UpdateUserCommand>
    {

        public UpdateUserCommandHandlerValidation()
        {


            RuleFor(x => x.UserName)
             .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
             .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Name)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Age)
            .InclusiveBetween(22, 60)
            .WithMessage("الرجاء إدخال قيمة العمر بين 22 و 60");



        }



    }
}
