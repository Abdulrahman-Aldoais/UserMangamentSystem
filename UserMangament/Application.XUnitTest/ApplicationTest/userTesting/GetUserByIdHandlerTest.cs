using Application.Features.Employees.Profiles;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Queries.Git;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using Application.XUnitTest.ApplicationTest.EntityMock;
using AutoMapper;
using Domain.Entities;
using Domain.Resources;
using Moq;
using System.Linq.Expressions;
using System.Net;
using Xunit;

namespace Application.XUnitTest.ApplicationTest.userTesting
{
    public class GetUserByIdHandlerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IUserReadRepository> _userReadRepositoryMock;
        private readonly IMapper _mapperMock;
        private readonly MappingProfile _mappingProfile;

        public GetUserByIdHandlerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userReadRepositoryMock = new Mock<IUserReadRepository>();
            _mappingProfile = new();
            var configutation = new MapperConfiguration(c => c.AddProfile(_mappingProfile));
            _mapperMock = new Mapper(configutation);


        }
    
        [Theory]
        [InlineData(6665)]
        [InlineData(54)]
        public async Task Handle_GetUser_By_Id_ShoudBe_Not_Null_Return_404_NotFound(int id)
        {
            var userMock = new UserMock();
            var query = new GetUserQuery { Id = id };
            _userServiceMock.Setup(x => x.GetUserByIdAsync(id)).Returns( Task.FromResult(userMock.users.FirstOrDefault(x => x.Id == id)));

            _userReadRepositoryMock.Setup(x => x.GetAsync(x => x.Id == id))
                           .ReturnsAsync();
            var handler = new GetUserQueryHandler(_userServiceMock.Object, _userReadRepositoryMock.Object);

            var result = await handler.Handle(query, default);

           // Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(SharedResourcesKeys.NotFound, result.Message);
        }

        //[Theory]
        //[InlineData(1)]





    }
}
