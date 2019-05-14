using HomeSwitchHome.Domain.Enumerations;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Application.Models.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Role { get; set; }
    }
}