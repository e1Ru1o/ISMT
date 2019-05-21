using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class ItinerarioDbAccess : IEntityDbAccess<Itinerario>
    {
        public readonly EfCoreContext _context;

        public ItinerarioDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Itinerario entity)
        {
            _context.Itinerarios.Add(entity);
        }

        public void Delete(Itinerario entity)
        {
            _context.Itinerarios.Remove(entity);
        }

        public IEnumerable<Itinerario> GetAll() => _context.Itinerarios;

        public Itinerario Update(Itinerario entity, Itinerario toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Estado de Viaje to be updated not exist");

            toUpd.FechaFin = entity.FechaFin ?? toUpd.FechaFin;
            toUpd.FechaInicio = entity.FechaInicio ?? toUpd.FechaInicio;
            toUpd.UsuarioID = entity.UsuarioID ?? toUpd.UsuarioID;
            toUpd.Viajes =  entity.Viajes == null ? entity.Viajes : (toUpd.Viajes.Concat(entity.Viajes)).ToList();

            _context.Itinerarios.Update(toUpd);
            return toUpd;
        }

        public Itinerario GetItinerario(Usuario usuario, DateTime? FechaInicio, DateTime? FechaFin)
        {
            return _context.Itinerarios.Where(i => i.UsuarioID == usuario.Id &&
                                                   i.FechaInicio == FechaInicio &&
                                                   i.FechaFin == FechaFin)
                                                   .Single();
        }

        public Itinerario GetItinerario(int ID)
        {
            return _context.Itinerarios.Find(ID);
        }
    }
}
