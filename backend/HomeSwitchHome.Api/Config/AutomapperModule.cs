using Autofac;
using AutoMapper;
using HomeSwitchHome.Infrastructure.Mapping;
using NLog;

namespace HomeSwitchHome.API.Config
{
    public class AutomapperModule : Module
    {
        private static readonly ILogger logger = LogManager.GetLogger(typeof(AutomapperModule).FullName);

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ManyToOneResolver<>)).As(typeof(ManyToOneResolver<>))
                   .InstancePerLifetimeScope();

            builder.Register(container =>
            {
                var configuration = AutomapperConfigurationProvider.BuildConfiguration();
                configuration.AssertConfigurationIsValid();
                return configuration;
            }).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve<IComponentContext>().Resolve))
                   .As<IMapper>().InstancePerLifetimeScope();

            logger.Info("Automapper loaded");
        }
    }
}