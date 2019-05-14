using System;
using System.Linq;
using NHibernate;
using NHibernate.Context;
using NHibernate.Validator.Exceptions;

namespace HomeSwitchHome.API.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void BeginTransaction();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _session;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            this._sessionFactory = sessionFactory;
        }

        public void BeginTransaction()
        {
            if (!CurrentSessionContext.HasBind(_sessionFactory))
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            _session = _sessionFactory.GetCurrentSession();
            _session.BeginTransaction();
        }

        public void Dispose()
        {
            if (CurrentSessionContext.HasBind(_sessionFactory))
            {
                var currentSession = CurrentSessionContext.Unbind(_sessionFactory);
                currentSession.Close();
                currentSession.Dispose();
            }
        }

        public void Commit()
        {
            var session = _sessionFactory.GetCurrentSession();
            var tx = session.Transaction;

            if (tx != null)
            {
                if (tx.IsActive)
                {
                    try
                    {
                        tx.Commit();
                    }
                    catch (InvalidStateException ex)
                    {
                        var values = ex.InvalidValues;
                        if (tx.IsActive)
                        {
                            tx.Rollback();
                        }

                        var invalidValues = values.Where(invalidValue => invalidValue.Entity != null)
                                                  .Aggregate(string.Empty, (current, invalidValue) => current +
                                                                                                      string
                                                                                                          .Format("{0} - {1}",
                                                                                                                  invalidValue
                                                                                                                      .PropertyName,
                                                                                                                  invalidValue
                                                                                                                      .Entity
                                                                                                                      .GetType()
                                                                                                                      .Name));

                        var applicationException =
                            new ApplicationException(
                                                     string
                                                         .Format("Invalid state while commiting the session. Invalid values: {0}",
                                                                 invalidValues), ex);

                        throw applicationException;
                    }
                    catch (Exception)
                    {
                        if (tx.IsActive)
                        {
                            tx.Rollback();
                        }

                        throw;
                    }
                }
            }
            else
            {
                session.Flush();
            }
        }

        public void Rollback()
        {
            var session = _sessionFactory.GetCurrentSession();
            var tx = session.Transaction;

            if (tx != null)
            {
                if (tx.IsActive)
                {
                    tx.Rollback();
                }
            }
        }
    }
}