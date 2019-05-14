using System;
using System.Collections.Generic;
using System.Linq;
using HomeSwitchHome.Application.Models.Users;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure;
using HomeSwitchHome.Infrastructure.Utils;
using NHibernate;

namespace HomeSwitchHome.Application.Services.Users
{
    public interface IUsersService
    {
        int Create(string userName, string name, string password, string role);
        User Authenticate(string email, string password);
        IEnumerable<User> GetAll(GetUsersFilter filter);
        User Get(int id);
        User Get(GetUsersFilter filter);
        RefreshToken GetRefresh(User user);
        void UpdateRefreshToken(User user, string token, DateTime expiration);
        void Update(int id, string name, bool isActive, string role, string password);
        void Delete(int id);
        bool Exist(int id);
    }

    public class UsersService : IUsersService
    {
        private ISession _session;
        private ISessionFactory _sessionFactory;

        public ISession Session
        {
            get { return _session = _session ?? _sessionFactory.GetCurrentSession(); }
        }

        public UsersService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public int Create(string userName, string name, string password, string role)
        {
            var entity = new User(userName)
            {
                Name = name,
                PasswordHash = PasswordHash.CreateHash(password),
                Role = role
            };

            Session.Save(entity);
            return entity.Id;
        }

        public User Authenticate(string userName, string password)
        {
            var user = Session.Query<User>()
                              .FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.IsActive);
            if (user != null && PasswordHash.ValidatePassword(password, user.PasswordHash))
                return user;

            return null;
        }

        public IEnumerable<User> GetAll(GetUsersFilter filter)
        {
            var query = Session.Query<User>();

            query = FilterQuery(filter, query);

            return query.ToArray();
        }

        public User Get(int id)
        {
            var filter = new GetUsersFilter {Id = id};
            return Get(filter);
        }

        public User Get(GetUsersFilter filter)
        {
            var query = Session.Query<User>();

            query = FilterQuery(filter, query);

            return query.SingleOrDefault();
        }

        public RefreshToken GetRefresh(User user)
        {
            return Session.Query<RefreshToken>().FirstOrDefault(x => x.User == user);
        }

        public void UpdateRefreshToken(User user, string token, DateTime expiration)
        {
            var exist = GetRefresh(user);
            if (exist != null)
            {
                exist.Token = token;
                exist.Expiration = expiration;
                return;
            }

            var newToken = new RefreshToken()
            {
                User = user,
                Token = token,
                Expiration = expiration
            };
            Session.Save(newToken);
        }

        public void Update(int id, string name, bool isActive, string role, string password)
        {
            var user = Session.Get<User>(id);
            user.Name = name;
            user.IsActive = isActive;
            user.Role = role;
            user.PasswordHash = PasswordHash.CreateHash(password);
            Session.Save(user);
        }

        public void Delete(int id)
        {
            var user = Session.Get<User>(id);
            var token = Session.Query<RefreshToken>().FirstOrDefault(x => x.User == user);
            if (token != null)
                Session.Delete(token);
            Session.Delete(user);
        }

        public bool Exist(int id)
        {
            return Session.Query<User>().Any(x => x.Id == id);
        }

        private IQueryable<User> FilterQuery(GetUsersFilter filter, IQueryable<User> query)
        {
            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive);

            if (filter.Id > 0)
                query = query.Where(x => x.Id == filter.Id);

            if (filter.UserName.IsNullOrEmpty() == false)
                query = query.Where(x => x.UserName.Contains(filter.UserName));
            return query;
        }
    }
}