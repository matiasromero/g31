using System;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Domain.Entities
{
    public class RefreshToken : Entity
    {
        public virtual User User { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime? Expiration { get; set; }
    }
}