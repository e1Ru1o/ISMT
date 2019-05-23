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
            
            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.Region = entity.Region ?? toUpd.Region;
            toUpd.Visas = toUpd.Visas == null ? entity.Visas : (toUpd.Visas.Concat(entity.Visas)).ToList();

            _context.Paises.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Country given its name.
        /// </summary>
        /// <param name="nombre">The name of the desired Country</param>
        /// <returns>The Country if its the only object with that identifier, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Pais GetPais(string nombre)
        {
            return _context.Paises.Where(p => p.Nombre == nombre).SingleOrDefault();
        }

        public Pais GetPais(string nombre, string nombreRegion)
        {
            return _context.Paises.Where(p => p.Nombre == nombre &&
                                              p.Region.Nombre == nombreRegion)
                                              .SingleOrDefault();
        }

    }

}
