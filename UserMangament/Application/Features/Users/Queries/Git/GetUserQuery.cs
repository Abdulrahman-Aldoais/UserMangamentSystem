using Application.Features.Users.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Users.Queries.Git
{
    public class GetUserQuery : IRequest<BaseCommandResponse<GetUserOutput>>
    {
        public int Id { get; set; }
    }
}
