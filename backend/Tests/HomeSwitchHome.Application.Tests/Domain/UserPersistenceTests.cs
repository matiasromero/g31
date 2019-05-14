using System.Linq;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Tests.Base;
using HomeSwitchHome.Tests.Builders;
using Moq;
using Xunit;

namespace HomeSwitchHome.Tests.Domain
{
    public class UserPersistenceTests : InMemoryDatabaseTest
    {
        public UserPersistenceTests() : base(typeof(UserPersistenceTests).Assembly)
        {
        }

        [Fact]
        public void SaveUser()
        {
            object id = null;

            ExecuteInTransaction(s =>
            {
                var user = new UserBuilder().WithBasicData().Build();
                id = s.Save(user);
            });

            ExecuteInTransaction(s =>
            {
                var user = s.Get<User>(id);

                Assert.Equal("username", user.UserName);
            });
        }
    }
}