using HomeSwitchHome.Domain.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace HomeSwitchHome.Domain.Persistence
{
    public class ResidenceMap : ClassMapping<Residence>
    {
        public ResidenceMap()
        {
            Table("Residences");

            Id(x => x.Id);

            Property(x => x.Code, x => x.Unique(true));
        }
    }
}