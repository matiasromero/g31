using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using HomeSwitchHome.Infrastructure;
using HomeSwitchHome.Domain.Base;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;

namespace HomeSwitchHome.API.Infrastructure
{
    public class AuditEventListener : DefaultFlushEntityEventListener, IPreUpdateEventListener, IPreInsertEventListener
    {
        //https://stackoverflow.com/questions/5087888/ipreupdateeventlistener-and-dynamic-update-true
        protected override void DirtyCheck(FlushEntityEvent e)
        {
            base.DirtyCheck(e);
            if (e.DirtyProperties != null &&
                e.DirtyProperties.Any() &&
                e.Entity is IHaveAuditInformation)
                e.DirtyProperties = e.DirtyProperties
                                     .Concat(GetAdditionalDirtyProperties(e)).ToArray();
        }

        static IEnumerable<int> GetAdditionalDirtyProperties(FlushEntityEvent @event)
        {
            yield return Array.IndexOf(@event.EntityEntry.Persister.PropertyNames,
                                       "UpdatedAt");
            yield return Array.IndexOf(@event.EntityEntry.Persister.PropertyNames,
                                       "UpdatedBy");
        }

        private readonly Func<ClaimsPrincipal> _principalAccessor;

        public AuditEventListener(Func<ClaimsPrincipal> principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        private ClaimsPrincipal GetPrincipal()
        {
            return _principalAccessor();
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(OnPreUpdate(@event));
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var principal = GetPrincipal();

            var time = DateTime.Now;
            var name = principal?.GetName();

            Set(@event.Persister, @event.State, "UpdatedAt", time);
            Set(@event.Persister, @event.State, "UpdatedBy", name);

            audit.UpdatedAt = time;
            audit.UpdatedBy = name;

            return false;
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(OnPreInsert(@event));
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var audit = @event.Entity as IHaveAuditInformation;
            if (audit == null)
                return false;

            var principal = GetPrincipal();

            var time = DateTime.Now;
            var name = principal?.GetName();

            Set(@event.Persister, @event.State, "CreatedAt", time);
            Set(@event.Persister, @event.State, "CreatedBy", name);

            audit.CreatedAt = time;
            audit.CreatedBy = name;

            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        public void Register(Configuration cfg)
        {
            var listeners = cfg.EventListeners;

            var flushEntityEventListener = listeners.FlushEntityEventListeners.FirstOrDefault();
            if (flushEntityEventListener != null &&
                flushEntityEventListener.GetType() != typeof(DefaultFlushEntityEventListener))
            {
                throw new Exception("A FlushEntityEventListener other than the default one is already registered");
            }

            listeners.FlushEntityEventListeners = new IFlushEntityEventListener[] {this};
            listeners.PreUpdateEventListeners = new[] {this}
                                                .Concat(listeners.PreUpdateEventListeners)
                                                .ToArray();
            listeners.PreInsertEventListeners = new[] {this}
                                                .Concat(listeners.PreInsertEventListeners)
                                                .ToArray();
        }
    }
}