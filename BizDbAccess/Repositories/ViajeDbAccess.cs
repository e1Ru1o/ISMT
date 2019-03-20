using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public void Delete(Viaje entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Viaje> GetAll()
        {
            return _context.Viajes;
        }

        public void Update(Viaje entity)
        {
            throw new NotImplementedException();
        }
    }

}
