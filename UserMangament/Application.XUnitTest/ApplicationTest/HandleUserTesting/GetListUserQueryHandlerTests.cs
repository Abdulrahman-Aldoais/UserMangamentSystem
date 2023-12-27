using Application.Features.Users.Dtos.GetList;
using Application.Features.Users.Profiles;
using Application.Features.Users.Queries.GitList;
using Application.Services.UserService;
using AutoMapper;
using Domain.Resources;
using Moq;
using System.Net;
using Xunit;

namespace Application.XUnitTest.ApplicationTest.HandleUserTesting
{
    public class GetListUserQueryHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly IMapper _mapperMock;
        private readonly MappingProfile _mappingProfile;
        public GetListUserQueryHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mappingProfile = new();
            var configutation = new MapperConfiguration(c => c.AddProfile(_mappingProfile));
            _mapperMock = new Mapper(configutation);
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
        public async Task Handle_UserList_ReturnsOkWhenGetAllUsers_Should_Not_Empty()
        {

            //Araing
            _userServiceMock.Setup(x => x.GetAllUserAsync()).ReturnsAsync(users);

            var query = new GetListUserQuery();
            var queryHandler = new GetListUserQueryHandler(_userServiceMock.Object);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotEmpty(result.Data);
            Assert.Null(result.Errors);
            Assert.Equal(SharedResourcesKeys.Success, result.Message);

        }

        [Fact]
        public async Task Handle_UserList_ReturnsBadRequestWhen_NotGetAllUsers_Should_Be_Empty()
        {
            var users2 = new List<GetListUserOutput>()
            {

            };
            _userServiceMock.Setup(x => x.GetAllUserAsync()).ReturnsAsync(users2);
            var queryHandler = new GetListUserQueryHandler(_userServiceMock.Object);
            var query = new GetListUserQuery();
            // Act
            var response = await queryHandler.Handle(query, CancellationToken.None);


            // Assert
            Assert.False(response.Success);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Empty(response.Data);
            Assert.Null(response.Errors);
            Assert.Equal(SharedResourcesKeys.BadRequest, response.Message);
        }

    }
}