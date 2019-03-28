using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    class VisaDbAccess : IEntityDbAccess<Visa>
    {
        private readonly EfCoreContext _context;

        public VisaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Visa entity)
        {
            _context.Visas.Add(entity);
        }

        public void Delete(Visa entity)
        {
            _context.Visas.Remove(entity);
        }

        public IEnumerable<Visa> GetAll() => _context.Visas;

        public Visa Update(Visa entity, Visa toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Visa to be updated not exist");

            toUpd.Name = entity.Name;
            toUpd.Paises = entity.Paises;
            toUpd.Pasaportes = entity.Pasaportes;

            _context.Visas.Update(toUpd);
            return toUpd;
        }

        public Visa GetVisa(string name)
        {
            return _context.Visas.Where(v => v.Name == name).SingleOrDefault();
        }
    }
}
