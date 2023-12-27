using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using Azure.Core;
using Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using System.Net.Mail;
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
        //[InlineData("abdulrahman")]
        [InlineData(" ")]
        public async Task Name_cannot_be_empty(string name)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Name = name,
                Age = 32,
                Email = "abdulrahman@gmail.com",
                UserName = "abdulrahman",
                Phone = "775115810"

            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.Should().NotBeNull();

            Assert.False(result.IsValid);
        }

        [Theory]
        [InlineData(32)]
        [InlineData(22)]
        public async Task Age_ValidationCanNot_be_Smole_Than_20_And_Largle_than_60(int age)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Age = age,
                Email = "abdulrahman@gmail.com",
                Name = "abdulrahman",
                UserName = "abdulrahman",
                Phone = "775115810"
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            Assert.True(result.IsValid);
            Assert.InRange(request.Age, 20, 60);

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
                Phone = "775115810"
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


        [Theory]
        [InlineData("775115810")] 
        public async void PhoneValidation_InvalidYemeniPhoneNumber_ReturnTrue(string phoneNumber)
        {
            var validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);
            // Arrange
            var model = new CreateUserCommand { 
                Phone = phoneNumber,
                Email = "aafgddga@gmail.com",
                Name = "abdulrahman",
                UserName = "abdulrahman",
                Age = 30,
               
            };

            // Act
          
            var result = await validator.ValidateAsync(model);
            Assert.True(result.IsValid ,result.Errors.ToString());
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("775115810545456")]
        [InlineData("12a34567")]
        public async void PhoneValidation_InvalidYemeniPhoneNumber_ReturnFale(string phoneNumber)
        {
            var validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);
            // Arrange
            var model = new CreateUserCommand
            {
                Phone = phoneNumber,
                Email = "aafgddga@gmail.com",
                Name = "abdulrahman",
                UserName = "abdulrahman",
                Age = 30,
            };

            // Act
            var result = await validator.ValidateAsync(model);

            Assert.False(result.IsValid, result.Errors.ToString());
        }


    }






}


