namespace HomeSwitchHome.Infrastructure.Domain
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        string UserName { get; }
        string Role { get; }
    }
}