using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using HomeSwitchHome.Application;

namespace HomeSwitchHome.API.Config
{
    public class AutomapperConfigurationProvider
    {
        public static MapperConfiguration BuildConfiguration(Func<Type, object> dependenciesFactory = null)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                // get all the AutoMapper Profile classes using reflection
                var profileTypes = typeof(AppConfiguration).Assembly.GetExportedTypes()
                                                           .Where(type =>
                                                                      !type.GetTypeInfo().IsAbstract &&
                                                                      type.GetTypeInfo().IsSubclassOf(typeof(Profile)))
                                                           .ToArray();
                foreach (var type in profileTypes)
                    cfg.AddProfile((Profile) Activator.CreateInstance(type));

                cfg.ConstructServicesUsing(dependenciesFactory);
            });

            return mapperConfiguration;
        }
    }
}