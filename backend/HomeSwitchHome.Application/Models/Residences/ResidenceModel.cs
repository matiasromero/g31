﻿using HomeSwitchHome.Domain.Models.Base;

namespace HomeSwitchHome.Application.Models.Products
{
    public class ResidenceModel : AuditableEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
    }
}