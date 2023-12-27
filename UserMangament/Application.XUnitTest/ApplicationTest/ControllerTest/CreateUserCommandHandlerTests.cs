using Application.Features.Users.Commands.Create;
using Application.Features.Users.Dtos.GetList;
using Application.Features.Users.Queries.GitList;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using Domain.Resources;
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

        public List<GetListUserOutput> users = new List<GetListUserOutput>
        {
            new GetListUserOutput
            {
                Id = 1,
                UserName = "abdulrahman",
                Age = 30,
                Phone = "123-456-7890",
                Email = "abdulrahman@exe.com",
                IsActive = true,
                AccountCancellationStatusBy = 1,
                CreatedDate = "1/22/2023",
                ModifiedDate = DateTime.Now,
                CreatedBy = 1,
                ModifiedBy = 1,
            },
            new GetListUserOutput
            {
                Id = 2,
                UserName = "ameen",
                Age = 25,
                Phone = "098-765-4321",
                Email = "ameen@exe.com",
                IsActive = true,
                AccountCancellationStatusBy = 1,
                CreatedDate = "1/22/2023",
                ModifiedDate = DateTime.Now,
                CreatedBy = 1,
                ModifiedBy = 1,
            }
        };


        [Fact]
        public async Task Handle_AddUser_WithTestData_Then_ReturnSuccess_Test()
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

        [Fact]
        public async Task Handle_GetAllUsers_ReturnsSuccessResponse_WhenUsersExist()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
           
            mockUserService.Setup(service => service.GetAllUserAsync()).ReturnsAsync(users);

            var handler = new GetListUserQueryHandler(mockUserService.Object);
            var query = new GetListUserQuery();

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(SharedResourcesKeys.Success, response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(users.Count, response.Data.Count);
        }

        [Fact]
        public async Task Handle_GetAllUsers_ReturnsBadRequest_WhenNoUsersExist()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUserAsync()).ReturnsAsync(new List<GetListUserOutput>());

            var handler = new GetListUserQueryHandler(mockUserService.Object);
            var query = new GetListUserQuery(); 

            // Act
            var response = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(SharedResourcesKeys.BadRequest, response.Message);
            Assert.NotNull(response.Data);
            Assert.Empty(response.Data);
        }
    }



}

