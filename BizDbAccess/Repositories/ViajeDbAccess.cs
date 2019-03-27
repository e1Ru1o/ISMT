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

            toUpd.Ciudades = entity.Ciudades;
            toUpd.Costo = entity.Costo;
            toUpd.FechaFin = entity.FechaFin;
            toUpd.FechaInicio = entity.FechaInicio;
            toUpd.Instituciones = entity.Instituciones;
            toUpd.MotivoViaje = entity.MotivoViaje;
            toUpd.Pais = entity.Pais;
            toUpd.Usuario = entity.Usuario;

            _context.Viajes.Update(toUpd);
            return toUpd;
        }

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
