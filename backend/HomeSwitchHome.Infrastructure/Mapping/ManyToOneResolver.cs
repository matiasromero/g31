using System;
using AutoMapper;
using HomeSwitchHome.Infrastructure.Domain;
using NHibernate;

namespace HomeSwitchHome.Infrastructure.Mapping
{
    public class ManyToOneResolver<T> : IMemberValueResolver<object, object, int?, T>
        where T : Entity
    {
        private readonly Lazy<ISession> session;

        public ManyToOneResolver(Lazy<ISession> session)
        {
            this.session = session;
        }

        public T Resolve(object source, object destination, int? sourceMember, T destMember, ResolutionContext context)
        {
            return sourceMember == null || sourceMember == 0 ? null : session.Value.Load<T>(sourceMember);
        }
    }
}