using HomeSwitchHome.Application.Models.Users;
using FluentValidation;

namespace HomeSwitchHome.Application.Validators.Users
{
    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}