using System;

namespace HomeSwitchHome.Domain.Base
{
    public interface IHaveCreationInformation
    {
        DateTime? CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }
}