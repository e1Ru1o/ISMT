using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class CiudadDbAccess : IEntityDbAccess<Ciudad>
    {
        private readonly EfCoreContext _context;

        public CiudadDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Ciudad entity)
        {
            _context.Ciudades.Add(entity);
        }

        public void Delete(Ciudad entity)
        {
            _context.Ciudades.Remove(entity);
        }

        public IEnumerable<Ciudad> GetAll() => _context.Ciudades;

        public Ciudad Update(Ciudad entity, Ciudad toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Ciudad to be updated no exist");

            toUpd.Nombre = entity.Nombre;
            toUpd.Pais = entity.Pais;

            _context.Ciudades.Update(toUpd);
            return toUpd;
        }

        public Ciudad GetCiudad(string nombre)
        {
            return _context.Ciudades.Where(c => c.Nombre == nombre).SingleOrDefault();
        }

        public Ciudad GetCiudad(string nombre, Pais pais)
        {
            return _context.Ciudades.Where(c => c.Nombre == nombre && c.Pais == pais).SingleOrDefault();
        }

        public IEnumerable<Ciudad> GetCiudadesByPais(Pais pais)
        {
             return _context.Ciudades.Where(c => c.Pais == pais).ToList();
        }
    }

}
