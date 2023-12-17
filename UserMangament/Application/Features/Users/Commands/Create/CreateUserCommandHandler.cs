using Application.Features.Users.Dtos.Get;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Domain.Resources;
using MediatR;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : BaseCommandResponseHandler,
        IRequestHandler<CreateUserCommand, BaseCommandResponse<GetUserOutput>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CreateUserCommandHandler(IUserService userService,
            IUserReadRepository userReadRepository,
            IMapper mapper

            )
        {

            _mapper = mapper;
            _userService = userService;
            _userReadRepository = userReadRepository;
        }
        #endregion

        #region Action
        public async Task<BaseCommandResponse<GetUserOutput>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetUserOutput>();
            var validator = new CreateUserCommandHandlerValidation(_userReadRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                response.Data = null;
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.UnprocessableEntity;
                response.Message = SharedResourcesKeys.validateAllExpectedFieldsReceivedInDatabase;
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

            }
            else
            {
                var userMapp = _mapper.Map<User>(request);
                userMapp.CreatedDate = DateTime.Now;

                var createResult = await _userService.AddNewUserAsync(userMapp);
                switch (createResult)
                {
                    case "Failed": return BadRequest<GetUserOutput>(SharedResourcesKeys.TryToRegisterAgain);
                    case "Success": return Success(response.Data);
                    default: return BadRequest<GetUserOutput>(SharedResourcesKeys.FaildToAddUser);
                }
            }
            return response;

        }
        #endregion

    }
}
