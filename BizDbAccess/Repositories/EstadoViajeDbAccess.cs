using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class EstadoViajeDbAccess : IEntityDbAccess<EstadoViaje>
    {
        public readonly EfCoreContext _context;

        public EstadoViajeDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(EstadoViaje entity)
        {
            _context.EstadosViaje.Add(entity);
        }

        public void Delete(EstadoViaje entity)
        {
            _context.EstadosViaje.Remove(entity);
        }

        public IEnumerable<EstadoViaje> GetAll() => _context.EstadosViaje.OrderBy(i => i.Nombre);

        public EstadoViaje Update(EstadoViaje entity, EstadoViaje toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Estado de Viaje to be updated not exist");

            toUpd.Nombre = entity.Nombre ?? toUpd.Nombre;

            _context.EstadosViaje.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a EstadoViaje given its name.
        /// </summary>
        /// <param name="nombre">The name of the desired EstadoViaje</param>
        /// <returns>The EstadoViaje if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public EstadoViaje GetEstadoViaje(string nombre)
        {
            return _context.EstadosViaje.Where(ev => ev.Nombre == nombre).SingleOrDefault();
        }
    }
}
