using System;

namespace HomeSwitchHome.Domain.Models.Base
{
    public abstract class AuditableEntityModel : EntityModel
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}