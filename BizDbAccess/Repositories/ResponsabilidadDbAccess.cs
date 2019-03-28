using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class ResponsabilidadDbAccess : IEntityDbAccess<Responsabilidad>
    {
        public readonly EfCoreContext _context;

        public ResponsabilidadDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Responsabilidad entity)
        {
            _context.Responsabilidades.Add(entity);
            _context.Commit();
        }

        public void Delete(Responsabilidad entity)
        {
            _context.Responsabilidades.Remove(entity);
            _context.Commit();
        }

        public IEnumerable<Responsabilidad> GetAll()
        {
            return _context.Responsabilidades.OrderBy(i => i.Nombre);
        }

        public void Update(Responsabilidad entity)
        {
            throw new NotImplementedException();
        }
    }
}
