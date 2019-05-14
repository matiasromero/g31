using NHibernate.Cfg.MappingSchema;

namespace HomeSwitchHome.Domain.Persistence
{
    public interface IMappingFixer
    {
        void Fix(HbmMapping mapping);
    }
}