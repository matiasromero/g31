using System;
using System.Linq;
using System.Security.Claims;

namespace HomeSwitchHome.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetId(this ClaimsPrincipal principal)
        {
            var id = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(id) ? (int?) null : int.Parse(id);
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.Any(x => x.Type == CustomClaimTypes.IsAdmin && x.Value == "true") ?? false;
        }

        public static string GetRole(this ClaimsPrincipal principal)
        {
            var claim = principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (claim == null)
                throw new InvalidOperationException("Role claim not found");

            return claim.Value;
        }
    }
}