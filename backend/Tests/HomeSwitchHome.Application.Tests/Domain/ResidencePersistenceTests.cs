using System.Linq;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Tests.Base;
using HomeSwitchHome.Tests.Builders;
using Xunit;

namespace HomeSwitchHome.Tests.Domain
{
    public class ResidencePersistenceTests : InMemoryDatabaseTest
    {
        public ResidencePersistenceTests() : base(typeof(ResidencePersistenceTests).Assembly)
        {
        }

        [Fact]
        public void SaveResidence()
        {
            object id = null;

            ExecuteInTransaction(s =>
            {
                var residence = new ResidenceBuilder().WithBasicData().Build();
                id = s.Save(residence);
            });

            ExecuteInTransaction(s =>
            {
                var residence = s.Get<Residence>(id);
                Assert.Equal("Casa del lago", residence.Name);
                Assert.Equal("00001", residence.Code);
            });
        }

        [Fact]
        public void SaveAndEditResidence()
        {
            object id = null;

            ExecuteInTransaction(s =>
            {
                var residence = new ResidenceBuilder().WithBasicData().Build();
                id = s.Save(residence);
            });

            ExecuteInTransaction(s =>
            {
                var residence = s.Get<Residence>(id);
                residence.Name = "Changed";
            });

            ExecuteInTransaction(s =>
            {
                var residence = s.Get<Residence>(id);
                Assert.Equal("Changed", residence.Name);
                Assert.Equal("00001", residence.Code);
            });
        }

        [Fact]
        public void DeleteResidence()
        {
            object id = null;

            ExecuteInTransaction(s =>
            {
                var residence = new ResidenceBuilder().WithBasicData().Build();
                id = s.Save(residence);
            });

            ExecuteInTransaction(s =>
            {
                var residence = s.Get<Residence>(id);
                s.Delete(residence);
            });

            ExecuteInTransaction(s =>
            {
                var residences = s.Query<Residence>().ToArray();
                Assert.Empty(residences);
            });
        }
    }
}