using System;

namespace HomeSwitchHome.Domain.Base
{
    public interface IHaveAuditInformation : IHaveCreationInformation
    {
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}