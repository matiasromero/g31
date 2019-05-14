using HomeSwitchHome.Domain.Base;
using NHibernate.Validator.Constraints;

namespace HomeSwitchHome.Domain.Entities
{
    public class Residence : AuditableEntity
    {
        public Residence()
        {
            IsAvailable = true;
        }

        [NotNullNotEmpty, Length(50)]
        public virtual string Name { get; set; }

        [NotNullNotEmpty, Length(10)]
        public virtual string Code { get; set; }

        [Length(500)]
        public virtual string Description { get; set; }

        public virtual string ImageUrl { get; set; }
        public virtual string ThumbnailUrl { get; set; }

        public virtual decimal Price { get; set; }

        public virtual bool IsAvailable { get; set; }
    }
}