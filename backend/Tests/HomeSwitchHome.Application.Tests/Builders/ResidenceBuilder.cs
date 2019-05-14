using HomeSwitchHome.Domain.Entities;

namespace HomeSwitchHome.Tests.Builders
{
    public class ResidenceBuilder
    {
        private Residence _entity = new Residence();

        public ResidenceBuilder Name(string name)
        {
            _entity.Name = name;
            return this;
        }

        public ResidenceBuilder Description(string description)
        {
            _entity.Description = description;
            return this;
        }

        public ResidenceBuilder ImageUrl(string url)
        {
            _entity.ImageUrl = url;
            return this;
        }

        public ResidenceBuilder ThumbnailUrl(string url)
        {
            _entity.ThumbnailUrl = url;
            return this;
        }

        public ResidenceBuilder Price(decimal price)
        {
            _entity.Price = price;
            return this;
        }

        public ResidenceBuilder Code(string code)
        {
            _entity.Code = code;
            return this;
        }

        public Residence Build()
        {
            return _entity;
        }

        /// <summary>
        /// Name: "Product"
        /// </summary>
        /// <returns></returns>
        public ResidenceBuilder WithBasicData()
        {
            _entity = new Residence()
            {
                Name = "Casa del lago",
                Code = "00001",
                Price = 10
            };

            return this;
        }
    }
}