namespace HomeSwitchHome.Application.Models.Users
{
    public class GetUsersFilter
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool? IsActive { get; set; }
    }
}