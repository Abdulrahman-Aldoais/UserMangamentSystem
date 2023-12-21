using Application.Features.Users.Dtos.GetList;
using Application.Features.Users.Queries.GitList;
using Application.Services.UserService;
using Domain.Resources;
using Moq;
using System.Net;
using Xunit;

namespace Application.XUnitTest.ApplicationTest
{
    public class GetListUserQueryHandlerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        public GetListUserQueryHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
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
        CreatedDate = DateTime.Now,
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
        CreatedDate = DateTime.Now,
        ModifiedDate = DateTime.Now,
        CreatedBy = 1,
        ModifiedBy = 1,
    }
};

        [Fact]
        public async Task Handle_ReturnsOkWhenGetAllUsers()
        {

            //Araing
            _userServiceMock.Setup(x => x.GetAllUserAsync()).ReturnsAsync(users);

            var query = new GetListUserQuery();
            var queryHandler = new GetListUserQueryHandler(_userServiceMock.Object);

            // Act
            var response = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(response.Data);
            Assert.Null(response.Errors);
            Assert.Equal(SharedResourcesKeys.Success, response.Message);


        }

        [Fact]
        public async Task Handle_ReturnsNotFountWhenNoUsers()
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