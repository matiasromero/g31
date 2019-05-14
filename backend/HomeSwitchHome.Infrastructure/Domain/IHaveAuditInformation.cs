using System;

namespace HomeSwitchHome.Infrastructure.Domain
{
    public interface IHaveAuditInformation : IHaveCreationInformation
    {
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}