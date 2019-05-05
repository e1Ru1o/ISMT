using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class ViajeDbAccess : IEntityDbAccess<Viaje>
    {
        private readonly EfCoreContext _context;

        public ViajeDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Viaje entity)
        {
            _context.Viajes.Add(entity);
        }

        public void Delete(Viaje entity)
        {
            _context.Viajes.Remove(entity);
        }

        public IEnumerable<Viaje> GetAll() => _context.Viajes;

        public Viaje Update(Viaje entity, Viaje toUpd)
        {
            if (toUpd == null)
                throw new Exception("Viaje to be updated no exist");

            toUpd.Costo = entity.Costo ?? toUpd.Costo;
            toUpd.FechaFin = entity.FechaFin ?? toUpd.FechaFin;
            toUpd.FechaInicio = entity.FechaInicio ?? toUpd.FechaFin;
            toUpd.MotivoViaje = entity.MotivoViaje ?? toUpd.MotivoViaje;
            toUpd.Pais = entity.Pais ?? toUpd.Pais;
            toUpd.Usuario = entity.Usuario ?? toUpd.Usuario;
            toUpd.Ciudades = toUpd.Ciudades == null ? entity.Ciudades : (toUpd.Ciudades.Concat(entity.Ciudades)).ToList();
            toUpd.Instituciones = toUpd.Instituciones == null ? entity.Instituciones : (toUpd.Instituciones.Concat(entity.Instituciones)).ToList();

            _context.Viajes.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Viaje given a user and a date representing a schedule.
        /// </summary>
        /// <param name="usuario">The user who has the trip in his behalf.</param>
        /// <param name="fechaInicio">The date of departure.</param>
        /// <param name="fechaFin">The date of arrival to the start point.</param>
        /// <returns>The Viaje if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Viaje GetViaje(Usuario usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            return _context.Viajes.Where(v => v.Usuario == usuario &&
                                              v.FechaFin == fechaFin &&
                                              v.FechaInicio == fechaInicio)
                                              .SingleOrDefault();
        }

        public Viaje GetViaje(long id)
        {            
            return _context.Viajes.Find(id); 
        }

    }

}
