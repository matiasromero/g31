namespace HomeSwitchHome.Application.Models.Residences
{
    public class GetResidencesFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsAvailable { get; set; }
    }
}