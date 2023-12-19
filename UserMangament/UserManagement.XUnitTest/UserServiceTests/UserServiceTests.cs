using Application.Repositories.UserRepository;
using Application.Services.UserService;
using AutoMapper;
using Domain.Entities;
using Moq;
using System.Linq.Expressions;

namespace Application.XUnitTest.UserServiceTests
{

    public class UserServiceTests
    {

        private readonly UserService _userService;
        private readonly Mock<IUserReadRepository> _userReadRepositoryMock = new Mock<IUserReadRepository>();
        private readonly Mock<IUserWriteRepository> _userWriteRepositoryMock = new Mock<IUserWriteRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public UserServiceTests()
        {
            _userService = new UserService(
       _userReadRepositoryMock.Object,
       _mapperMock.Object,
       _userWriteRepositoryMock.Object
             );
        }

        [Fact]
        public async Task GetAllUserAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "John Doe",
                    Age = 30,
                    Phone = "123-456-7890",
                    Email = "johndoe@example.com",
                    IsActive = true,
                    AccountCancellationStatusBy =1,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy =1,

                },
                new User
                {
                    Id = 2,
                    UserName = "Jane Doe",
                    Age = 25,
                    Phone = "098-765-4321",
                    Email = "janedoe@example.com",
                    IsActive = true,
                    AccountCancellationStatusBy =1,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = 1,
                    ModifiedBy =1,
                }
            };

            var userQueryable = users.AsQueryable();

            _userReadRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<User, bool>>>()))
                         .Returns(userQueryable);

            //_userReadRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<User, bool>>>()))
            //           .Returns(await Task.FromResult(userQueryable));

            // Act
            var result = await _userService.GetAllUserAsync();

            // Assert
            Assert.Equal(users.Count, result.Count);
        }

        //[Fact]
        //public async Task GetUserByIdAsync_ShouldReturnUser()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        Id = 1,
        //        UserName = "John Doe",
        //        Age = 30,
        //        Phone = "123-456-7890",
        //        Email = "johndoe@example.com",
        //        IsActive = true
        //    };

        //    // Act
        //    var result = await _userService.GetUserByIdAsync(user.Id);

        //    // Assert
        //    Assert.Equal(user, result);
        //}

        //[Fact]
        //public async Task AddNewUserAsync_ShouldAddUser()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        UserName = "John Doe",
        //        Age = 30,
        //        Phone = "123-456-7890",
        //        Email = "johndoe@example.com",
        //        IsActive = true
        //    };

        //    // Act
        //    var result = await _userService.AddNewUserAsync(user);

        //    // Assert
        //    Assert.Equal(result, "User added successfully");
        //}

        //[Fact]
        //public async Task UpdateInformationUser_ShouldUpdateUser()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        Id = 1,
        //        UserName = "John Doe",
        //        Age = 30,
        //        Phone = "123-456-7890",
        //        Email = "johndoe@example.com",
        //        IsActive = true
        //    };

        //    // Act
        //    var result = await _userService.UpdateInformationUser(user);

        //    // Assert
        //    Assert.Equal(result, "User updated successfully");
        //}
    }
}


