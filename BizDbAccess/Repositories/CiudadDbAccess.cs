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

            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;
            toUpd.Pais = entity.Pais ?? toUpd.Pais;

            _context.Ciudades.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a city given its name.
        /// </summary>
        /// <param name="nombre">The name of the desired city.</param>
        /// <returns>The city if its the only object with that identifier, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Ciudad GetCiudad(string nombre)
        {
            return _context.Ciudades.Where(c => c.Nombre == nombre).SingleOrDefault();
        }

        /// <summary>
        /// Get a city given its name and a country.
        /// </summary>
        /// <param name="nombre">The name of the desired city.</param>
        /// <param name="pais">The country of the city.</param>
        /// <returns>The city if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
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
