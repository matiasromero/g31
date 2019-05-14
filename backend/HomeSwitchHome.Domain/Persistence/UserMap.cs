using HomeSwitchHome.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace HomeSwitchHome.Domain.Persistence
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(x => x.Id);

            Property(x => x.UserName, x => x.Unique(true));
        }
    }
}