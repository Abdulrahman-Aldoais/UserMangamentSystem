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
            
            var response = new BaseCommandResponse<GetUserOutput>();
            var validator = new GetUserQueryHandlerValidation(_userReadRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            // Validate the query using the injected validator

            if (!validatorResult.IsValid)
            {
                response.Data = null;
                response.Success = false;
                response.Message = SharedResourcesKeys.IsNotExist;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                // Ensure _userReadRepository is not null before using it
                if (_userReadRepository != null)
                {
                    var result = await _userReadRepository.GetAsync(x => x.Id == request.Id);

                    if (result != null)
                    {
                        var userData = await _userService.GetUserByIdAsync(request.Id);

                        response.Id = userData.Id;
                        response.Data = userData;
                        response.Success = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                        response.Message = SharedResourcesKeys.Success;
                        response.Errors = null;
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = SharedResourcesKeys.IsNotExist;
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        response.Errors = new List<string> { "User not found." };
                    }
                }
                else
                {
                    // Handle the case where _userReadRepository is null
                    response.Data = null;
                    response.Success = false;
                    response.Message = SharedResourcesKeys.IsNotExist;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Errors = new List<string> { "_userReadRepository is null." };
                }
            }
            return response;
        }
    }
}

