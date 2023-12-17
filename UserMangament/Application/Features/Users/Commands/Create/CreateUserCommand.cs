using Application.Features.Users.Dtos.Get;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<BaseCommandResponse<GetUserOutput>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? CreatedBy { get; set; }

    }
}
