using System.Linq;
using HomeSwitchHome.Application.Models.Users;
using HomeSwitchHome.Application.Validators.Users;
using HomeSwitchHome.Tests.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace HomeSwitchHome.Tests.Validators
{
    [Collection("CreateUserValidatorCollection")]
    public class CreateUserRequestValidatorTests : InMemoryDatabaseTest
    {
        public CreateUserRequestValidatorTests() : base(typeof(CreateUserRequestValidatorTests).Assembly)
        {
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("name", true)]
        [InlineData("valid name", true)]
        [InlineData("123456789012345678901234567890123456789012345678901", false)]
        public void Name_validations(string name, bool pass)
        {
            ExecuteInTransaction(s =>
            {
                var validator = new CreateUserRequestValidator(s);

                if (pass)
                    validator.ShouldNotHaveValidationErrorFor(p => p.Name, name);
                else
                    validator.ShouldHaveValidationErrorFor(p => p.Name, name);
            });
        }

        [Theory]
        [InlineData("this.is.a.valid.username", true)]
        [InlineData("invalid2", true)]
        [InlineData("valid_name", true)]
        [InlineData("valid-name", true)]
        [InlineData("valid._-name", true)]
        [InlineData("invalid_name@", false)]
        [InlineData("invalid name", false)]
        [InlineData("", false)]
        public void UserName_validations(string email, bool pass)
        {
            ExecuteInTransaction(s =>
            {
                var validator = new CreateUserRequestValidator(s);
                if (pass)
                    validator.ShouldNotHaveValidationErrorFor(p => p.UserName, email);
                else
                    validator.ShouldHaveValidationErrorFor(p => p.UserName, email);
            });
        }

        [Fact]
        public void Should_fail_when_password_doesnt_match_with_confirmation()
        {
            ExecuteInTransaction(s =>
            {
                var validator = new CreateUserRequestValidator(s);
                //Arrange
                var request = new CreateUserRequest
                {
                    Name = "Test",
                    UserName = "user.Name",
                    Password = "Password1234",
                    PasswordConfirmation = "1234Password"
                };

                //Act
                var validation = validator.Validate(request);

                //Assert
                Assert.False(validation.IsValid);
                Assert.NotNull(validation.Errors);
                Assert.NotEmpty(validation.Errors);
                Assert.NotNull(validation.Errors.FirstOrDefault(x => x.ErrorMessage == "Passwords do not match"));
            });
        }

        [Fact]
        public void Should_pass_when_all_fields_are_filled_correctly()
        {
            ExecuteInTransaction(s =>
            {
                var validator = new CreateUserRequestValidator(s);
                //Arrange
                var cmd = new CreateUserRequest
                {
                    Name = "Test",
                    UserName = "userName",
                    Password = "Password1234",
                    PasswordConfirmation = "Password1234",
                    Role = "admin"
                };

                //Act
                var validation = validator.Validate(cmd);

                //Assert
                Assert.True(validation.IsValid);
            });
        }
    }
}