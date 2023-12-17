using Application.Features.Users.Dtos.Get;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.Users.Queries.Git
{
    public class GetUserQueryHandler : BaseCommandResponseHandler, IRequestHandler<GetUserQuery, BaseCommandResponse<GetUserOutput>>
    {
        private readonly IUserService _userService;
        private readonly IUserReadRepository _userReadRepository;
        public GetUserQueryHandler(IUserService userService, IUserReadRepository userReadRepository)
        {
            _userService = userService;
            _userReadRepository = userReadRepository;
        }
        public async Task<BaseCommandResponse<GetUserOutput>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var respons = new BaseCommandResponse<GetUserOutput>();
            var validator = new GetUserQueryHandlerValidation(_userReadRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                respons.Data = null;
                respons.Success = false;
                respons.Message = SharedResourcesKeys.IsNotExist;
                respons.StatusCode = System.Net.HttpStatusCode.NotFound;
                respons.Success = false;
                respons.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();

            }
            else
            {
                var result = await _userService.GetUserByIdAsync(request.Id);

                respons.Id = result.Id;
                respons.Data = result;
                respons.Success = true;
                respons.StatusCode = System.Net.HttpStatusCode.OK;
                respons.Message = SharedResourcesKeys.Success;
                respons.Errors = null;
            }
            return respons;

        }
    }
}
