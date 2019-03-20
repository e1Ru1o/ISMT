using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class InstitucionDbAccess : IEntityDbAccess<Institucion>
    {
        private readonly EfCoreContext _context;

        public InstitucionDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Institucion entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Institucion entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Institucion> GetAll()
        {
            return _context.Instituciones;
        }

        public void Update(Institucion entity)
        {
            throw new NotImplementedException();
        }
    }

}
