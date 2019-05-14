using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace HomeSwitchHome.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            return WebHost.CreateDefaultBuilder(args)
                          .ConfigureLogging((hostingContext, logging) =>
                          {
                              logging.AddConfiguration(hostingContext
                                                       .Configuration.GetSection("Logging"));
                          })
                          .UseNLog()
                          .UseStartup<Startup>();
        }
    }
}