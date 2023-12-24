using Application.Features.Departments.Profiles;
using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;
using System;

namespace Modle.XUnitTest
{
    public class UnitTest1
    {
        private readonly Mock<IUserReadRepository> _userReadRepositoryMock;
        private readonly CreateUserCommandHandlerValidation _validator;

        public UnitTest1()
        {
            _userReadRepositoryMock = new Mock<IUserReadRepository>();
            _validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("íÓÈáÓ")]
        [InlineData(" ")]
        public async Task Name_cannot_be_empty(string name)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Name = name,
                Age = 32,
                // Assign other properties as needed for the command
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

    }

}