using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class CiudadDbAccess : IEntityDbAccess<Ciudad>
    {
        private readonly EfCoreContext _context;

        public CiudadDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Ciudad entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ciudad entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ciudad> GetAll()
        {
            return _context.Ciudades;
        }

        public void Update(Ciudad entity)
        {
            throw new NotImplementedException();
        }
    }

}
