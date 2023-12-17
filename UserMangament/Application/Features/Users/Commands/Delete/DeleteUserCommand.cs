using Core.Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest<BaseCommandResponse<int>>
    {
        public int Id { get; set; }
    }
}
