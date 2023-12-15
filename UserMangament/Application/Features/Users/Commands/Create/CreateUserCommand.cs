﻿using Application.Features.Users.Dtos.Get;
using Core.Application.Responses;
using MediatR;


namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<BaseCommandResponse<GetUserOutput>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = false;
        public int AccountCancellationStatusBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}