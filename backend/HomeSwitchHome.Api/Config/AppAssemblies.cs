using System.Linq;
using System.Reflection;
using HomeSwitchHome.Application;
using HomeSwitchHome.Infrastructure;
using HomeSwitchHome.Domain.Entities;

namespace HomeSwitchHome.API.Config
{
    public static class AppAssemblies
    {
        static AppAssemblies()
        {
            All = new[] {typeof(Startup), typeof(User), typeof(AppConfiguration), typeof(PasswordHash)}
                  .Select(x => x.Assembly).ToArray();
        }

        public static Assembly[] All { get; }
    }
}