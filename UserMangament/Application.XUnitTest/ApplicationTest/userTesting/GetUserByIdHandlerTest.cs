using Application.Features.Employees.Profiles;
using Application.Features.Users.Dtos.Get;
using Application.Features.Users.Queries.Git;
using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using Domain.Resources;
using FluentValidation.TestHelper;
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
        private readonly GetUserQueryHandlerValidation _validator;
        private readonly IMapper _mapperMock;
        private readonly MappingProfile _mappingProfile;
        private GetUserQueryHandler _handler;


        public GetUserByIdHandlerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userReadRepositoryMock = new Mock<IUserReadRepository>();
            _mappingProfile = new();
            var configutation = new MapperConfiguration(c => c.AddProfile(_mappingProfile));
            _mapperMock = new Mapper(configutation);
            _validator = new GetUserQueryHandlerValidation(_userReadRepositoryMock.Object);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task Handle_GetUser_By_Id_ShoudBe_Not_Null_Return_404_NotFound(int id)
        {
            _handler = new GetUserQueryHandler(_userServiceMock.Object, _userReadRepositoryMock.Object);

            _userServiceMock.Setup(x => x.GetUserByIdAsync(id)).ReturnsAsync((GetUserOutput)null);
            _userReadRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync((User)null);

            var query = new GetUserQuery { Id = id };
            var validatorResult = await _validator.TestValidateAsync(query, default);

            var handleResult = await _handler.Handle(query, CancellationToken.None);

            // التحقق من النتائج
            Assert.Equal(HttpStatusCode.NotFound, handleResult.StatusCode);
            Assert.Equal(SharedResourcesKeys.NotFound, handleResult.Message);
        }





    }
}
