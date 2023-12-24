using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.XUnitTest.ApplicationTest.ControllerTest
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidUserData_When_Creat_ReturnsSuccessResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockUserReadRepository = new Mock<IUserReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var createUserCommandHandler = new CreateUserCommandHandler(
                mockUserService.Object,
                mockUserReadRepository.Object,
                mockMapper.Object
            );

            var validCreateUserCommand = new CreateUserCommand
            {
                UserName = "abdulrahman",
                Name = "abdulrahman5*",
                Email = "abdulrahman@example.com",
                Phone = "775115810",
                CreatedBy = null,
                Age = 24
                // Add other necessary properties
            };

            var cancellationToken = new CancellationToken(); // Use a cancellation token

            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(new User());
            mockUserService.Setup(us => us.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync("Success");

            // Act
            var result = await createUserCommandHandler.Handle(validCreateUserCommand, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Null(result.Errors);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            // Add more assertions based on your expected behavior
        }


        [Fact]
        public async Task Handle_ValidUserData_When_Creat_ReturnsFileadResponse()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockUserReadRepository = new Mock<IUserReadRepository>();
            var mockMapper = new Mock<IMapper>();

            var createUserCommandHandler = new CreateUserCommandHandler(
                mockUserService.Object,
                mockUserReadRepository.Object,
                mockMapper.Object
            );

            var validCreateUserCommand = new CreateUserCommand
            {

                Name = "abdulrahman5*",
                Email = "abdulrahman@example.com",
                Phone = "775115810",
                CreatedBy = null,
                Age = 24
                // Add other necessary properties
            };

            var cancellationToken = new CancellationToken(); // Use a cancellation token

            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(new User());
            mockUserService.Setup(us => us.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync("Success");

            // Act
            var result = await createUserCommandHandler.Handle(validCreateUserCommand, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotNull(result.Errors);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
           
        }
    }
}
