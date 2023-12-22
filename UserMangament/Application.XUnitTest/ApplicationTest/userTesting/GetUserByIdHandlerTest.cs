using Application.Features.Employees.Profiles;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Queries.Git;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Resources;
using Moq;
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
        public List<GetUserOutput> users = new List<GetUserOutput>
{
    new GetUserOutput
    {
        Id = 1,
        UserName = "abdulrahman",
        Age = 30,
        Phone = "123-456-7890",
        Email = "abdulrahman@exe.com",
        IsActive = true,
        AccountCancellationStatusBy = 1,
        CreatedDate =  DateTime.Now,
        ModifiedDate = DateTime.Now,
        CreatedBy = 1,
        ModifiedBy = 1,
    },
    new GetUserOutput
    {
        Id = 2,
        UserName = "ameen",
        Age = 25,
        Phone = "098-765-4321",
        Email = "ameen@exe.com",
        IsActive = true,
        AccountCancellationStatusBy = 1,
        CreatedDate =  DateTime.Now,
        ModifiedDate = DateTime.Now,
        CreatedBy = 1,
        ModifiedBy = 1,
    }
};
        [Theory]
        [InlineData(1)]
        public async Task Handle_GetUser_By_Id_ShoudBe_Not_Null_Return_404_NotFound(int id)
        {
            var query = new GetUserQuery { Id = id };
            _userServiceMock.Setup(x => x.GetUserByIdAsync(id)).Returns(Task.FromResult(users.FirstOrDefault(x => x.Id == id)));
            var handler = new GetUserQueryHandler(_userServiceMock.Object, _userReadRepositoryMock.Object);

            var result = await handler.Handle(query, default);

            Assert.False(result.Success);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal(SharedResourcesKeys.NotFound, result.Message);
        }

        //[Theory]
        //[InlineData(1)]





    }
}
