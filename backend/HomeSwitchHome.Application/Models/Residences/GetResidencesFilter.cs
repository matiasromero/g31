namespace HomeSwitchHome.Application.Models.Residences
{
    public class GetResidencesFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsAvailable { get; set; }
    }
}