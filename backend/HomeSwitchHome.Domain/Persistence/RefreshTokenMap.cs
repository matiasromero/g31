using HomeSwitchHome.Domain.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HomeSwitchHome.Domain.Persistence
{
    public class RefreshTokenMap : ClassMapping<RefreshToken>
    {
        public RefreshTokenMap()
        {
            Table("RefreshTokens");

            Id(x => x.Id);
        }
    }
}