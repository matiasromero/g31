using System;
using Autofac;
using HomeSwitchHome.Application;
using HomeSwitchHome.Infrastructure.NHibernate;
using HomeSwitchHome.API.Infrastructure;
using HomeSwitchHome.Domain.Persistence;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NLog;
using Environment = NHibernate.Cfg.Environment;
using IContainer = Autofac.IContainer;

namespace HomeSwitchHome.API.Config
{
    public static class AppBootstrapper
    {
        private static readonly Logger logger = LogManager.GetLogger(typeof(AppBootstrapper).FullName);

        private static IContainer container;

        public static IContainer GetContainer()
        {
            if (container == null)
                throw new Exception("InitializeContainer was not called");

            return container;
        }

        public static void InitializeContainer(AppConfiguration configuration,
                                               Action<ContainerBuilder> configurator = null)
        {
            logger.Info("Initializating app container");

            var builder = new ContainerBuilder();

            //https://github.com/jbogard/MediatR/issues/128
            builder.RegisterSource(new ScopedContravariantRegistrationSource());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new NHibernateModule(configuration.ConnectionString,
                                                        configuration.RecreateDatabase,
                                                        false,
                                                        configuration.NHibernateTimeout));

            configurator?.Invoke(builder);

            container = builder.Build();
        }

        public static Configuration CreateConfiguration(IComponentContext ctx, string connectionString = null,
                                                        int? timeout = null)
        {
            logger.Info("Using connection string {connectionString}", connectionString);

            var configuration = new Configuration()
                .SetProperty(Environment.SqlExceptionConverter, typeof(MsSqlExceptionConverter).AssemblyQualifiedName);

            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionProvider<DriverConnectionProvider>();
                db.Dialect<MsSql2012Dialect>();

                db.Driver<Sql2008ClientDriver>();
                db.ConnectionString = connectionString;
                db.BatchSize = 100;
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.Timeout = (byte) (timeout ?? 10);
                db.LogFormattedSql = true;
                db.LogSqlInConsole = false;
            });

            new CustomizedEntitiesMap().Configure(configuration);

            ctx.Resolve<AuditEventListener>().Register(configuration);


            return configuration;
        }

        public static void Release()
        {
            if (container != null)
                container.Dispose();
        }
    }
}