using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class PasaporteDbAccess : IEntityDbAccess<Pasaporte>
    {
        private readonly EfCoreContext _context;

        public PasaporteDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Pasaporte entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pasaporte entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pasaporte> GetAll()
        {
            return _context.Pasaportes;
        }

        public void Update(Pasaporte entity)
        {
            throw new NotImplementedException();
        }
    }

}
