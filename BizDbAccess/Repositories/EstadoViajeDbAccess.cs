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
            _context.Commit();
        }

        public void Delete(EstadoViaje entity)
        {
            _context.EstadosViaje.Remove(entity);
            _context.Commit();
        }

        public IEnumerable<EstadoViaje> GetAll()
        {
            return _context.EstadosViaje.OrderBy(i => i.Nombre);
        }

        public void Update(EstadoViaje entity)
        {
            throw new NotImplementedException();
        }
    }
}
