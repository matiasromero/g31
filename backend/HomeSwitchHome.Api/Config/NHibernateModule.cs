using System;
using System.Data.SqlClient;
using System.Linq;
using Autofac;
using HomeSwitchHome.Infrastructure.NHibernate;
using HomeSwitchHome.API.Infrastructure;
using HomeSwitchHome.Domain.Persistence;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using NLog;
using Polly;
using Environment = NHibernate.Cfg.Environment;

namespace HomeSwitchHome.API.Config
{
    public class NHibernateModule : Module
    {
        private static readonly Logger logger = LogManager.GetLogger(typeof(NHibernateModule).FullName);

        private readonly string _connectionString;
        private readonly bool _isTestEnvironment;
        private readonly bool _recreateDatabase;
        private readonly int? _timeout;

        public NHibernateModule(string connectionString, bool recreateDatabase, bool isTestEnvironment = false,
                                int? timeout = null)
        {
            _connectionString = connectionString;
            _isTestEnvironment = isTestEnvironment;
            _recreateDatabase = recreateDatabase;
            _timeout = timeout;
        }

        protected override void Load(ContainerBuilder builder)
        {
            NHibernateLogger.SetLoggersFactory(new NLogLoggerFactory());

            builder.RegisterType<AuditEventListener>().SingleInstance();
            builder.RegisterAssemblyTypes(AppAssemblies.All)
                   .Where(x => x.Name.EndsWith("Listener") &&
                               x.GetInterfaces()
                                .Any(i => i.Namespace.Equals(typeof(IPreUpdateEventListener).Namespace)))
                   .AsSelf()
                   .SingleInstance();

            builder.Register(c =>
            {
                var cfg = CreateConfiguration(c, _connectionString, _isTestEnvironment);
                if (_recreateDatabase)
                {
                    logger.Warn("Recreating db schema");
                    new SchemaExport(cfg).Execute(false, true, false);
                }

                return CreateConfiguration(c, _connectionString, _isTestEnvironment);
            }).SingleInstance();

            builder.Register(c =>
                   {
                       var factory = Policy
                                     .Handle<SqlException>()
                                     .WaitAndRetry(5,
                                                   retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                                   (ex, retryIn) => logger.Warn(ex,
                                                                                "Error while building session factory ... retrying in {seconds} seconds",
                                                                                retryIn.TotalSeconds))
                                     .Execute(() => c.Resolve<Configuration>()
                                                     .SetProperty("current_session_context_class", "thread_static")
                                                     .BuildSessionFactory());

                       return factory;
                   })
                   .As<ISessionFactory>()
                   .SingleInstance();

            //builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerLifetimeScope();
            //builder.Register(c => c.Resolve<Lazy<ISession>>().Value).As<ISession>().InstancePerLifetimeScope();

            builder.RegisterType<SessionContext>().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var sessionFactory = c.Resolve<ISessionFactory>();

                var ctx = c.Resolve<SessionContext>();
                ctx.Put(c.Resolve<ILifetimeScope>());

                return new Lazy<ISession>(() =>
                {
                    var session = sessionFactory.OpenSession();
                    session.BindContext(ctx);
                    session.BeginTransaction();
                    return session;
                });
            }).InstancePerLifetimeScope();

            builder.Register(c => c.Resolve<Lazy<ISession>>().Value).As<ISession>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            logger.Info("NHibernate components registered");
        }

        public Configuration CreateConfiguration(IComponentContext ctx, string connectionString = null,
                                                 bool isTestEnvironment = false)
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
                db.Timeout = (byte) (_timeout ?? 10);
                db.LogFormattedSql = true;
                db.LogSqlInConsole = false;
            });

            new CustomizedEntitiesMap().Configure(configuration);

            ctx.Resolve<AuditEventListener>().Register(configuration);

            return configuration;
        }
    }
}