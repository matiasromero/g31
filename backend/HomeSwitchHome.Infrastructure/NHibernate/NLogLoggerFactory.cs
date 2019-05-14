using System;
using NHibernate;
using NLog;

namespace HomeSwitchHome.Infrastructure.NHibernate
{
    public class NLogLoggerFactory : INHibernateLoggerFactory
    {
        public INHibernateLogger LoggerFor(Type type)
        {
            return new NLogLogger(LogManager.GetLogger(type.FullName));
        }

        public INHibernateLogger LoggerFor(string keyName)
        {
            return new NLogLogger(LogManager.GetLogger(keyName));
        }

        public class NLogLogger : INHibernateLogger
        {
            private readonly Logger logger;

            public NLogLogger(Logger logger)
            {
                this.logger = logger;
            }

            public bool IsEnabled(NHibernateLogLevel logLevel)
            {
                return logger.IsEnabled(GetLogLevel(logLevel));
            }

            public void Log(NHibernateLogLevel logLevel, NHibernateLogValues state, Exception exception)
            {
                logger.Log(new LogEventInfo
                {
                    LoggerName = logger.Name,
                    Exception = exception,
                    Message = state.Format,
                    Parameters = state.Args,
                    Level = GetLogLevel(logLevel)
                });
            }

            private LogLevel GetLogLevel(NHibernateLogLevel logLevel)
            {
                if (logLevel == NHibernateLogLevel.Trace)
                    return LogLevel.Trace;
                if (logLevel == NHibernateLogLevel.Debug)
                    return LogLevel.Debug;
                if (logLevel == NHibernateLogLevel.Info)
                    return LogLevel.Info;
                if (logLevel == NHibernateLogLevel.Warn)
                    return LogLevel.Warn;
                if (logLevel == NHibernateLogLevel.Error)
                    return LogLevel.Error;
                if (logLevel == NHibernateLogLevel.Fatal)
                    return LogLevel.Fatal;
                if (logLevel == NHibernateLogLevel.None)
                    return LogLevel.Off;
                throw new ArgumentException(nameof(logLevel));
            }
        }
    }
}