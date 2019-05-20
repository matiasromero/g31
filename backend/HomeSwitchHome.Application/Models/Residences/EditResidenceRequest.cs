using Microsoft.AspNetCore.Http;

namespace HomeSwitchHome.Application.Models.Residences
{
    public class EditResidenceRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public IFormFile File { get; set; }
    }
}