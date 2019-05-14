using System.Runtime.CompilerServices;
using Autofac;
using MediatR;
using NHibernate;

namespace HomeSwitchHome.Infrastructure.NHibernate
{
    public static class SessionExtensions
    {
        static readonly ConditionalWeakTable<ISession, SessionContext> SessionsContexts =
            new ConditionalWeakTable<ISession, SessionContext>();

        public static IMediator GetMediator(this ISession session)
        {
            return session.GetContext().GetValueOrDefault<ILifetimeScope>()?.ResolveOptional<IMediator>();
        }

        public static void BindContext(this ISession session, SessionContext sessionContext)
        {
            lock (SessionsContexts)
            {
                SessionsContexts.Add(session, sessionContext);
            }
        }

        public static SessionContext GetContext(this ISession session)
        {
            lock (SessionsContexts)
            {
                return SessionsContexts.GetOrCreateValue(session);
            }
        }
    }
}