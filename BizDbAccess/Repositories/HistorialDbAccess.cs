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

        }

        public IEnumerable<Historial> GetAll()
        {
            return _context.Historial.OrderBy(item => item.Fecha);
        }

        public Historial Update(Historial entity, Historial toUpd)
        {
            return null;
        }
    }
}
