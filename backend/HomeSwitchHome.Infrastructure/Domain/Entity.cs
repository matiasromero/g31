namespace HomeSwitchHome.Infrastructure.Domain
{
    public interface IGenericEntity
    {
    }

    public interface IEntity : IGenericEntity
    {
        int Id { get; set; }
        int Version { get; }
    }

    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }
        public virtual int Version { get; set; }
    }
}