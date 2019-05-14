using HomeSwitchHome.Infrastructure.Domain;
using HomeSwitchHome.Domain.Base;
using NHibernate.Validator.Constraints;

namespace HomeSwitchHome.Domain.Entities
{
    public class User : AuditableEntity, IUser
    {
        public User()
        {
            IsActive = true;
        }

        public User(string userName)
            : this()
        {
            UserName = userName;
        }

        [NotNullNotEmpty]
        public virtual string PasswordHash { get; set; }

        [NotNullNotEmpty, Length(50)]
        public virtual string UserName { get; set; }

        [NotNullNotEmpty, Length(50)]
        public virtual string Name { get; set; }

        public virtual string Role { get; set; }

        [NotNull]
        public virtual bool IsActive { get; set; }
    }
}