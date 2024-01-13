using Application.Features.Users.Dtos.Get;
using Application.Repositories.UserRepository;
using AutoMapper;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHabdler : IRequestHandler<DeleteUserCommand, BaseCommandResponse<int>>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IMapper _mapper;
        public DeleteUserCommandHabdler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMapper mapper)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _mapper = mapper;

        }
        public async Task<BaseCommandResponse<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse<int>();
            var validator = new DeletUserCommandHabdlerValidation(_userReadRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                response.Data = request.Id;
                response.Success = false;
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                response.Message = null;
                response.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {

                var getUserFromDataBase = await _userReadRepository.GetAsync(x => x.Id.Equals(request.Id));
                await _userWriteRepository.DeleteAsync(getUserFromDataBase);
                var userMapp = _mapper.Map<GetUserOutput>(getUserFromDataBase);

                response.Id = userMapp.Id;
                response.Data = userMapp.Id;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = SharedResourcesKeys.Deleted;
                response.Errors = null;

            }
            return response;

        }
    }
}
