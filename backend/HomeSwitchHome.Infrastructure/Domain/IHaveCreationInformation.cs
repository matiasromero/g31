using System;

namespace HomeSwitchHome.Infrastructure.Domain
{
    public interface IHaveCreationInformation
    {
        DateTime? CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}