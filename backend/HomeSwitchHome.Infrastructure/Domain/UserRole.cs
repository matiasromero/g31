using NHibernate.Linq.Functions;

namespace HomeSwitchHome.Infrastructure.Domain
{
    public static class UserRole
    {
        public const string Admin = "admin";
        public const string Premium = "premium";
        public const string Basic = "basic";

        public const string GetAll = Admin + "," + Premium + "," + Basic;
    }
}