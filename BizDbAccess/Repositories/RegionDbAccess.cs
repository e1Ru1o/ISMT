using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class RegionDbAccess : IEntityDbAccess<Region>
    {
        private readonly EfCoreContext _context;

        public RegionDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Region entity)
        {
            _context.Regiones.Add(entity);
        }

        public void Delete(Region entity)
        {
            _context.Regiones.Remove(entity);
        }

        public IEnumerable<Region> GetAll() => _context.Regiones;

        public Region Update(Region entity, Region toUpd)
        {
            if (toUpd == null)
                throw new Exception("Pasaporte to be updated no exist");

            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.Paises = entity.Paises == null ? entity.Paises : (toUpd.Paises.Concat(entity.Paises)).ToList();
            toUpd.Visas = toUpd.Visas == null || toUpd.Visas.Count() == 0 ? entity.Visas : (toUpd.Visas.Concat(entity.Visas)).ToList();

            _context.Regiones.Update(toUpd);
            return toUpd;
        }

        public Region GetRegion(string nombre)
        {
            return _context.Regiones.Where(r => r.Nombre == nombre).SingleOrDefault();
        }
    }

}
