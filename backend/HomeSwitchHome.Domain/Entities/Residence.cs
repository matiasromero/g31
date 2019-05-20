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
        public virtual string Description { get; set; }

        [Length(500)]
        public virtual string Address { get; set; }

        public virtual string ImageUrl { get; set; }
        public virtual string ThumbnailUrl { get; set; }


        public virtual bool IsAvailable { get; set; }
    }
}