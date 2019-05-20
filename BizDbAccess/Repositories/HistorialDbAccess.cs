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
            throw new NotImplementedException();
        }

        public Historial GetHistorial(Estado estadoViaje, Itinerario viaje)
        {
            return _context.Historial.Where(h => h.Estado == estadoViaje &&
                                                 h.Itinerario.Usuario.Id == viaje.Usuario.Id)
                                                 .SingleOrDefault();
        }
    }
}
