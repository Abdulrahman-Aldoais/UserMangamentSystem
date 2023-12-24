using Application.Features.Users.Commands.Create;
using Application.Repositories.UserRepository;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task Age_ValidationCanNot_be_Smole_Than_20_And_Largle_than_60( int age)
        {
            // Arrange
            var request = new CreateUserCommand
            {
                Age = age,
            };

            // Act
            var result = await _validator.TestValidateAsync(request);

            // Assert
            if (age < 20 || age > 60)
            {
                result.ShouldHaveValidationErrorFor(x => x.Age)
                    .WithErrorMessage("يجب أن يكون العمر بين 20 و 60");
            }
            else
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Age);
            }

        }

        [Theory]
        [InlineData("abdulrahman@gmail.com")]
        [InlineData("abdulrahman @gmail .com")]
        [InlineData(null)]
        [InlineData("")] 
        public async Task EmailValidation(string email)
        {
            // Arrange
            var validator = new CreateUserCommandHandlerValidation(_userReadRepositoryMock.Object);

            var request = new CreateUserCommand
            {
                Email = email,
                Name = "abdulrahman",
                UserName = "abdulrahman",
                Age = 30,
                Phone = "123-456-7890"
            };

            // Act
            var result = await validator.ValidateAsync(request);

            // 
            if (string.IsNullOrWhiteSpace(email) || email.Trim() != email)
            {
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreateUserCommand.Email)
                    && error.ErrorMessage == "البريد الإلكتروني فارغ أو يحتوي على مسافات فارغة أو مسافات نهائية");
            }
            else
            {
               
                Assert.True(result.IsValid);
            }
        }





    }
}
