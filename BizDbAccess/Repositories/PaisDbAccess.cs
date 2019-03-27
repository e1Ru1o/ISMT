using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class PaisDbAccess : IEntityDbAccess<Pais>
    {
        private readonly EfCoreContext _context;

        public PaisDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Pais entity)
        {
            _context.Paises.Add(entity);
        }

        public void Delete(Pais entity)
        {
            _context.Paises.Remove(entity);
        }

        public IEnumerable<Pais> GetAll() => _context.Paises;

        public Pais Update(Pais entity, Pais toUpd)
        {
            if (toUpd == null)
                throw new Exception("Pais to be updated no exist");

            toUpd.Nombre = entity.Nombre;
            toUpd.Visas = entity.Visas;

            _context.Paises.Update(toUpd);
            return toUpd;
        }

        public Pais GetPais(string nombre)
        {
            return _context.Paises.Where(p => p.Nombre == nombre).SingleOrDefault();
        }

    }

}
