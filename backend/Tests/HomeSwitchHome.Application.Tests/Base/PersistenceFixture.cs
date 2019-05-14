using System;
using System.Reflection;
using HomeSwitchHome.Domain.Persistence;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.SqlCommand;
using NHibernate.Tool.hbm2ddl;
using NLog;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace HomeSwitchHome.Tests.Base
{
    [Collection("Sequential")]
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration _configuration;
        protected static ISessionFactory _sessionFactory;
        private readonly ISession _session;

        private ITestOutputHelper output = new TestOutputHelper();

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration();
                _configuration.DataBaseIntegration(db =>
                {
                    db.ConnectionProvider<DriverConnectionProvider>();
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionString = "Data Source=:memory:;New=True;";
                    db.BatchSize = 100;
                    db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                    db.LogFormattedSql = true;
                    db.LogSqlInConsole = true;
                    db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                });
                _configuration.AddAssembly(assemblyContainingMapping);

                new CustomizedEntitiesMap().Configure(_configuration);

                _configuration.SetInterceptor(new LoggingInterceptor());

                _sessionFactory = _configuration.SetProperty("current_session_context_class", "thread_static")
                                                .BuildSessionFactory();
            }

            _session = _sessionFactory.OpenSession();

            if (!CurrentSessionContext.HasBind(_sessionFactory))
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            new SchemaExport(_configuration).Execute(true, true, false, _session.Connection, Console.Out);
        }

        public static void DisposeCurrentSessionFactory()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);

            currentSession.Close();
            currentSession.Dispose();
        }

        public void Dispose()
        {
            //_session.Dispose();
            DisposeCurrentSessionFactory();
        }

        public void ClearSession()
        {
            _session.Clear();
        }

        public void ExecuteInTransaction(Action<ISession> action)
        {
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    action(_session);
                    tx.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        tx.Rollback();
                    }
                    catch (Exception)
                    {
                        // ignore exception during rollback to avoid masking original exception
                    }

                    throw;
                }
            }

            ClearSession();
        }
    }

    public class LoggingInterceptor : EmptyInterceptor
    {
        private static readonly Logger logger = LogManager.GetLogger(typeof(LoggingInterceptor).FullName);

        public override SqlString OnPrepareStatement(SqlString sql)
        {
            var query = sql.ToString();
            logger.Info(query);
            return sql;
        }
    }
}