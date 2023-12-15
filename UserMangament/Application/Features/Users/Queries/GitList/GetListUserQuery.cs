using Application.Features.Users.Dtos.GetList;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.GitList
{
    public class GetListUserQuery : IRequest<BaseCommandResponse<List<GetListUserOutput>>>
    {

    }
}
