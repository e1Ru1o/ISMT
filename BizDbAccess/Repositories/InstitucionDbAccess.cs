using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class InstitucionDbAccess : IEntityDbAccess<Institucion>
    {
        private readonly EfCoreContext _context;

        public InstitucionDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Institucion entity)
        {
            _context.Instituciones.Add(entity);
        }

        public void Delete(Institucion entity)
        {
            _context.Instituciones.Remove(entity);
        }

        public IEnumerable<Institucion> GetAll() => _context.Instituciones;

        public Institucion Update(Institucion entity, Institucion toUpd)
        {
            if (toUpd == null)
                throw new Exception("Institucion to be updated no exist");

            toUpd.Nombre = entity.Nombre;

            _context.Instituciones.Update(toUpd);
            return toUpd;
        }

        public Institucion GetInstitucion(string nombre)
        {
            return _context.Instituciones.Where(i => i.Nombre == nombre).SingleOrDefault();
        }
    }

}
