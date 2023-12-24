﻿using Application.Repositories.UserRepository;
using Domain.Resources;
using FluentValidation;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandlerValidation : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserReadRepository _userReadRepository;

        public CreateUserCommandHandlerValidation(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;

            RuleFor(x => x.UserName)
             .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
             .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Name)
          .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
          .NotNull().WithMessage(SharedResourcesKeys.Required);


            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

           
            RuleFor(x => x.Age)
            .InclusiveBetween(22, 60)
            .WithMessage("الرجاء إدخال قيمة بين 22 و 60");

            RuleFor(x => x)
                 .MustAsync(NameCanNotBeDuplicatedWhenInserted)
                 .WithMessage("اسم المستخدم موجود");


            RuleFor(x => x)
                .MustAsync(EmailCanNotBeDuplicatedWhenInserted)
                .WithMessage("البريد الإلكتروني موجود");


            RuleFor(x => x.Email)
    .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
    .NotNull().WithMessage(SharedResourcesKeys.Required)
    .Must(email => !string.IsNullOrWhiteSpace(email) && email.Trim() == email && IsValidEmail(email))
    .WithMessage(SharedResourcesKeys.InvalidEmailFormat);



        }


        private async Task<bool> NameCanNotBeDuplicatedWhenInserted(CreateUserCommand e, CancellationToken token)
        {
            var result = await _userReadRepository.GetAsync(x => x.Name == e.Name);
            return result == null;
        }

        private async Task<bool> EmailCanNotBeDuplicatedWhenInserted(CreateUserCommand e, CancellationToken token)
        {
            var result = await _userReadRepository.GetAsync(x => x.Email == e.Email);
            return result == null;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9\._%+-]+)@([a-zA-Z0-9\._%+-]+)\.([a-zA-Z]{2,4})$");

                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }


    }
}
