using System.Collections.Generic;
using System.Security.Claims;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Infrastructure
{
    public static class ClaimsPrincipalHelper
    {
        public static readonly ClaimsPrincipal Null = Create(null, null, null);

        public static readonly ClaimsPrincipal System = Create("system", 1, "System");

        public static ClaimsPrincipal Create<TUser>(TUser user)
            where TUser : IUser
        {
            return Create(user.UserName, user.Id, user.Name, user.Role);
        }

        public static ClaimsPrincipal Create(string email, int? id = null, string givenName = null,
                                             string userRole = UserRole.Admin)
        {
            var claims = new List<Claim>();
            if (email != null)
                claims.Add(new Claim(ClaimTypes.Name, email, ClaimValueTypes.String));

            if (id != null)
                claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString(), ClaimValueTypes.String));
            if (givenName != null)
                claims.Add(new Claim(ClaimTypes.GivenName, givenName, ClaimValueTypes.String));

            var claim = new Claim(ClaimTypes.Role, userRole, ClaimValueTypes.String);
            claims.Add(claim);

            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}