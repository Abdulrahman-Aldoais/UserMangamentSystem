using Application.Features.Users.Dtos.Get;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using MediatR;
using School.Domain.Resources;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : BaseCommandResponseHandler,
        IRequestHandler<CreateUserCommand, BaseCommandResponse<GetUserOutput>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CreateUserCommandHandler(IUserService userService, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
        {

            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Action
        public async Task<BaseCommandResponse<GetUserOutput>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetUserOutput>();
            var validator = new CreateUserCommandHandlerValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

            }
            else
            {
                var userMapp = _mapper.Map<User>(request);
                var createResult = await _userService.AddNewUserAsync(userMapp);
                switch (createResult)
                {
                    case "EmailIsExist": return BadRequest<GetUserOutput>(SharedResourcesKeys.EmailIsExist);
                    case "UserNameIsExist": return BadRequest<GetUserOutput>(SharedResourcesKeys.UserNameIsExist);
                    case "ErrorInCreateUser": return BadRequest<GetUserOutput>(SharedResourcesKeys.FaildToAddUser);
                    case "Failed": return BadRequest<GetUserOutput>(SharedResourcesKeys.TryToRegisterAgain);
                    case "Success": return Success(response.Data);
                    default: return BadRequest<GetUserOutput>(createResult);
                }
            }
            return response;

        }
        #endregion

    }
}
