using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure;
using HomeSwitchHome.Infrastructure.Domain;

namespace HomeSwitchHome.Tests.Builders
{
    public class UserBuilder
    {
        private User _entity = new User();

        public UserBuilder Name(string name)
        {
            _entity.Name = name;
            return this;
        }

        public UserBuilder UserName(string userName)
        {
            _entity.UserName = userName;
            return this;
        }

        public UserBuilder Status(bool status)
        {
            _entity.IsActive = status;
            return this;
        }

        public UserBuilder Password(string password)
        {
            _entity.PasswordHash = PasswordHash.CreateHash(password);
            return this;
        }

        public User Build()
        {
            return _entity;
        }

        /// <summary>
        /// Name: "Test", UserName: "test@email.com", IsActive: true, PasswordHash: hash of "password"
        /// </summary>
        /// <returns></returns>
        public UserBuilder WithBasicData()
        {
            _entity = new User("Test")
            {
                UserName = "username",
                PasswordHash = PasswordHash.CreateHash("password"),
                Role = UserRole.Admin
            };

            return this;
        }
    }
}