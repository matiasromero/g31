using System.Collections.Generic;
using System.Linq;
using HomeSwitchHome.Application.Models.Residences;
using HomeSwitchHome.Domain.Entities;
using HomeSwitchHome.Infrastructure.Utils;
using NHibernate;

namespace HomeSwitchHome.Application.Services.Residences
{
    public interface IResidencesService
    {
        Residence Create(string name, string address, string description);

        IEnumerable<Residence> GetAll(GetResidencesFilter filter);
        Residence Get(int id);
        Residence Get(GetResidencesFilter filter);

        void Update(int id, string name, string address, string description, bool isAvailable);

        bool Exist(int id);
        void UpdateFileName(int id, string fileName, string thumb);
        void Delete(int id);
    }

    public class ResidencesService : IResidencesService
    {
        private ISession _session;
        private readonly ISessionFactory _sessionFactory;

        public ISession Session
        {
            get { return _session = _session ?? _sessionFactory.GetCurrentSession(); }
        }

        public ResidencesService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Residence Create(string name, string address, string description)
        {
            var entity = new Residence()
            {
                Name = name,
                Address = address,
                Description = description,
            };

            Session.Save(entity);
            return entity;
        }

        public void UpdateFileName(int id, string fileName, string thumb)
        {
            var product = Session.Get<Residence>(id);
            product.ImageUrl = fileName;
            product.ThumbnailUrl = thumb;
        }

        public IEnumerable<Residence> GetAll(GetResidencesFilter filter)
        {
            var query = Session.Query<Residence>();

            query = FilterQuery(filter, query);

            return query.ToArray();
        }

        public Residence Get(int id)
        {
            var filter = new GetResidencesFilter() {Id = id};
            return Get(filter);
        }

        public Residence Get(GetResidencesFilter filter)
        {
            var query = Session.Query<Residence>();

            query = FilterQuery(filter, query);

            return query.SingleOrDefault();
        }

        public void Update(int id, string name, string address, string description, bool isAvailable)
        {
            var residence = Session.Get<Residence>(id);
            residence.Name = name;
            residence.Address = address;
            residence.Description = description;
            residence.IsAvailable = isAvailable;
            Session.Save(residence);
        }

        public void Delete(int id)
        {
            var product = Session.Get<Residence>(id);
            Session.Delete(product);
        }

        public bool Exist(int id)
        {
            return Session.Query<Residence>().Any(x => x.Id == id);
        }

        private IQueryable<Residence> FilterQuery(GetResidencesFilter filter, IQueryable<Residence> query)
        {
            if (filter.Id > 0)
                query = query.Where(x => x.Id == filter.Id);

            if (filter.Name.IsNullOrEmpty() == false)
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (filter.IsAvailable.HasValue)
                query = query.Where(x => x.IsAvailable == filter.IsAvailable);

            return query;
        }
    }
}