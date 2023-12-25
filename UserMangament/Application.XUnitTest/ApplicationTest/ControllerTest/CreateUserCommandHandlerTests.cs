using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
using UserMangament.Controllers;
using Xunit;

namespace Application.XUnitTest.ApplicationTest.ControllerTest
{
    public class CreateUserCommandHandlerTests
    {
        //private readonly UsersController _controller;
        //private readonly IMediator _mockMediator;

        private readonly Mock<IHttpContextAccessor> _contextAccessor;

        public CreateUserCommandHandlerTests()
        {
            var mockMediator = new Mock<IMediator>();
            //_mockMediator = mockMediator.Object;

            _contextAccessor = new Mock<IHttpContextAccessor>();
        }




        [Fact]
        public async Task Handle_ValidUserData_When_Creat_Returns_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();

            var mockUserService = new Mock<IUserService>();
            var mockUserReadRepository = new Mock<IUserReadRepository>();
            var mockMapper = new Mock<IMapper>();

            //var baseController = new BaseController();
            //    baseController._mediator = _mockMediator;

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
            };

            var cancellationToken = new CancellationToken();

            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(new User());
            mockUserService.Setup(us => us.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync("Success");

            // Act
            var result = await createUserCommandHandler.Handle(validCreateUserCommand, cancellationToken);

            var controller = new UsersController(_contextAccessor.Object);


            //var resultController = await controller.AddUser(validCreateUserCommand) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Null(result.Errors);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            //Assert.Equal("Index", resultController.ActionName, ignoreCase: true);
        }


        [Fact]
        public async Task Handle_ValidUserData_When_Creat_ReturnsFilead()
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
                Phone = " ",
                CreatedBy = null,
                Age = 99

            };

            var cancellationToken = new CancellationToken(); // Use a cancellation token

            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>())).Returns(new User());
            mockUserService.Setup(us => us.AddNewUserAsync(It.IsAny<User>())).ReturnsAsync("Failed");

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
