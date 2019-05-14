using FluentValidation;
using NHibernate;

namespace HomeSwitchHome.Application.Validators.Users
{
    public class DeleteUserRequestValidator : AbstractValidator<int>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Id is required");
        }
    }
}