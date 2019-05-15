using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class Region_VisaDbAccess : IEntityDbAccess<Region_Visa>
    {
        private readonly EfCoreContext _context;

        public Region_VisaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Region_Visa entity)
        {
            _context.Regiones_Visa.Add(entity);
        }

        public void Delete(Region_Visa entity)
        {
            _context.Regiones_Visa.Remove(entity);
        }

        public IEnumerable<Region_Visa> GetAll() => _context.Regiones_Visa;

        public Region_Visa Update(Region_Visa entity, Region_Visa toUpd)
        {
            if (toUpd == null)
                throw new Exception("Region_Visa to be updated no exist");

            toUpd.Region = entity.Region ?? toUpd.Region;
            toUpd.Visa = entity.Visa ?? toUpd.Visa;

            _context.Regiones_Visa.Update(toUpd);
            return toUpd;
        }

        public Region_Visa GetRegion_Visa(string RegionName, string VisaName)
        {
            return _context.Regiones_Visa.Where(rv => rv.Region.Nombre == RegionName &&
                                                      rv.Visa.Name == VisaName)
                                                      .SingleOrDefault();
        }
    }
}
