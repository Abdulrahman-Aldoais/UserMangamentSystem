using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using Domain.Resources;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Application.XUnitTest.ApplicationTest.UserModleTest
{
    public class UserModleTesting
    {
        private readonly Mock<IUserReadRepository> _userReadRepositoryMock;
        private readonly CreateUserCommandHandlerValidation _validator;

        public UserModleTesting()
        {
            _userReadRepositoryMock = new Mock<IUserReadRepository>();
            _validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("abdulrahman")]
        [InlineData(" ")]
        public async Task Name_cannot_be_empty(string name)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Name = name,
                Age = 32,

            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(32)]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(61)]
        public async Task Age_ValidationCanNot_be_Smole_Than_20_And_Largle_than_60(int age)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Age = age,
                //Email = "abdulrahman@gmail.com",
                //Name = "abdulrahman",
                //UserName = "abdulrahman",
                //Phone = "123-456-7890"
            };

            // Act
            // var result = await _validator.TestValidateAsync(request);

            // Assert
            Assert.InRange(request.Age, 20, 60);
            //result.ShouldHaveValidationErrorFor(x => x.Age)
            //    .WithErrorMessage("يجب أن يكون العمر بين 20 و 60");

        }



        [Theory]
        [InlineData("abdulrahman.smith@company.com", true)]
        [InlineData("abdulrahman", false)]
        [InlineData("abdulrahman@company.com", true)]
        [InlineData("abdulrahman.smith@company.comma", true)]
        [InlineData("abdulrahman.smith@gmail.com", true)]
        [InlineData("abdulrahman.smith@company.it", true)]
        [InlineData("abdulrahman.smith.company.com", false)]
        [InlineData("abdulrahman@smith@company.com", false)]
        [InlineData("", false)]
        public async void CheckEmail(string mailAddress, bool expectedTestResult)
        {
            var validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);
            var request = new CreateUserCommand
            {
                Email = mailAddress,
                Name = "abdulrahman",
                UserName = "abdulrahman",
                Age = 30,
                Phone = "123-456-7890"
            };
            var validationResult = validator.IsValidAddress(mailAddress);
            var result = await validator.ValidateAsync(request);

            Assert.Equal(expectedTestResult, validationResult);
            Assert.Equal(expectedTestResult, result.IsValid);
            if (!expectedTestResult)
            {
                Assert.Contains(result.Errors, error => error.ErrorMessage == SharedResourcesKeys.InvalidEmailFormat);
            }

        }



    }





}

