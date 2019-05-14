using HomeSwitchHome.Domain.Models.Base;

namespace HomeSwitchHome.Application.Models.Users
{
    public class UserModel : AuditableEntityModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}