using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class PaisDbAccess : IEntityDbAccess<Pais>
    {
        private readonly EfCoreContext _context;

        public PaisDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Pais entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pais entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pais> GetAll()
        {
            return _context.Paises;
        }

        public void Update(Pais entity)
        {
            throw new NotImplementedException();
        }
    }

}
