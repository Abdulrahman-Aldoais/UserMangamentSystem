using Application.Features.Users.Dtos.Get;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateUserCommandHandler : BaseCommandResponseHandler, IRequestHandler<UpdateUserCommand, BaseCommandResponse<GetUserOutput>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserReadRepository _userReadRepository;
        public UpdateUserCommandHandler(IMapper mapper, IUserService userService, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _userReadRepository = userReadRepository;
        }
        async Task<BaseCommandResponse<GetUserOutput>> IRequestHandler<UpdateUserCommand, BaseCommandResponse<GetUserOutput>>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<GetUserOutput>();
            var validator = new UpdateUserCommandHandlerValidation();
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
                var getUserInformationById = await _userReadRepository.GetAsync(x => x.Id == request.Id);

                getUserInformationById.Id = request.Id;
                getUserInformationById.Age = request.Age;
                getUserInformationById.Name = request.Name;
                getUserInformationById.Phone = request.Phone;
                getUserInformationById.Email = request.Email;
                getUserInformationById.UserName = request.UserName;
                getUserInformationById.ModifiedDate = DateTime.Now;
                getUserInformationById.AccountCancellationStatusBy = 1;
                getUserInformationById.ModifiedBy = 1;

                var createResult = await _userService.UpdateInformationUser(getUserInformationById);

                var userMapp = _mapper.Map<GetUserOutput>(request);
                response.Data = userMapp;
                response.Success = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = SharedResourcesKeys.Success;
                switch (createResult)
                {
                    case "Failed": return BadRequest<GetUserOutput>(SharedResourcesKeys.TryToRegisterAgain);
                    case "Success": return Success(response.Data);
                    default: return BadRequest<GetUserOutput>(SharedResourcesKeys.FaildToAddUser);
                }
            }
            return response;
        }
    }
}
