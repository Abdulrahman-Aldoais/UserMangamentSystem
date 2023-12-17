using Application.Features.Users.Dtos.GetList;
using Application.Services.UserService;
using Core.Application.Responses;
using Domain.Resources;
using MediatR;

namespace Application.Features.Users.Queries.GitList
{
    public class GetListUserQueryHandler : BaseCommandResponseHandler,
                                           IRequestHandler<GetListUserQuery, BaseCommandResponse<List<GetListUserOutput>>>
    {

        private readonly IUserService _userService;

        public GetListUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<BaseCommandResponse<List<GetListUserOutput>>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var respons = new BaseCommandResponse<List<GetListUserOutput>>();

            var result = await _userService.GetAllUserAsync();

            if (!result.Any())
            {
                respons.Success = false;
                respons.StatusCode = System.Net.HttpStatusCode.BadRequest;
                respons.Data = new List<GetListUserOutput>();
                respons.Errors = null;
                respons.Message = SharedResourcesKeys.BadRequest;


            }
            else
            {
                respons.Data = result;
                respons.StatusCode = System.Net.HttpStatusCode.OK;
                respons.Message = SharedResourcesKeys.Success;
                respons.Success = true;
                respons.Errors = null;


            }

            return respons;
        }
    }
}
