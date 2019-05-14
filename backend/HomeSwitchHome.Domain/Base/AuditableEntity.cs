using System;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Domain.Base
{
    public abstract class AuditableEntity : Entity, IHaveAuditInformation
    {
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedAt { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
}