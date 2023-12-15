using Application.Repositories.UserRepository;
using FluentValidation;
using School.Domain.Resources;

namespace Application.Features.Users.Queries.Git
{
    public class GetUserQueryHandlerValidation : AbstractValidator<GetUserQuery>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserQueryHandlerValidation(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;


            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
                .NotNull().WithMessage(SharedResourcesKeys.Required);



            RuleFor(x => x)
                   .MustAsync(IdIsNotExists)
                   .WithMessage(SharedResourcesKeys.IsNotExist);

        }

        private async Task<bool> IdIsNotExists(GetUserQuery e, CancellationToken token)
        {
            var result = await _userReadRepository.GetAsync(x => x.Id == e.Id);
            return result != null;
        }



    }
}
