using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;

using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class HistorialDbAccess : IEntityDbAccess<Historial>
    {
        private readonly EfCoreContext _context;

        public HistorialDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Historial entity)
        {
            _context.Historial.Add(entity);
        }

        public void Delete(Historial entity)
        {
            _context.Historial.Remove(entity);
        }

        public IEnumerable<Historial> GetAll() =>  _context.Historial.OrderBy(item => item.Fecha);

        public Historial Update(Historial entity, Historial toUpd)
        {
            if (toUpd == null)
                throw new Exception("Historial to be updated no exist");

            toUpd.EstadoViaje = entity.EstadoViaje ?? toUpd.EstadoViaje;
            toUpd.Viaje = entity.Viaje ?? toUpd.Viaje;

            _context.Historial.Update(toUpd);
            return toUpd;
        }

        public Historial GetHistorial(string estadoViaje, Itinerario viaje)
        {
            return _context.Historial.Where(h => h.EstadoViaje == estadoViaje &&
                                                 h.Viaje.Usuario.Id == viaje.Usuario.Id)
                                                 .SingleOrDefault();
        }
    }
}
