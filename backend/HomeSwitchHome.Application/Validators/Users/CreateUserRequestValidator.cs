using System.Linq;
using HomeSwitchHome.Application.Models.Users;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure.Domain;
using HomeSwitchHome.Infrastructure.Utils;
using FluentValidation;
using NHibernate;

namespace HomeSwitchHome.Application.Validators.Users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private ISession _session;

        public CreateUserRequestValidator(ISession session)
        {
            _session = session;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                                .MaximumLength(50).WithMessage("Name cannot be greater than 50");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required")
                                    .MaximumLength(50).WithMessage("UserName cannot be greater than 50")
                                    .Matches(@"^[0-9a-zA-Z\._-]+$")
                                    .WithMessage("UserName should contain letters, dot and dash")
                                    .Must(BeUnique).WithMessage("UserName should be unique");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                    .Length(4, 20).WithMessage("Password should be between 4 and 20 characters");
            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty().WithMessage("Confirmation Password is required")
                .Equal(x => x.Password)
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Passwords do not match");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(BeAValidRole).WithMessage("Invalid Role");
        }

        private bool BeAValidRole(string role)
        {
            return role.IsNullOrEmpty() == false && UserRole.GetAll.Contains(role);
        }

        private bool BeUnique(string userName)
        {
            return _session.Query<User>().Any(x => x.UserName == userName) == false;
        }
    }
}