using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BizDbAccess.Repositories
{
    public class VisaDbAccess : IEntityDbAccess<Visa>
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

        public IEnumerable<Visa> GetAll() => _context.Visas.OrderBy(v => v.Name);

        public Visa Update(Visa entity, Visa toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Visa to be updated not exist");

            toUpd.Name = entity.Name ?? toUpd.Name;
            toUpd.Paises = toUpd.Paises == null ? entity.Paises : (toUpd.Paises.Concat(entity.Paises)).ToList();
            toUpd.Usuarios = toUpd.Usuarios == null ? entity.Usuarios : (toUpd.Usuarios.Concat(entity.Usuarios)).ToList();

            _context.Visas.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Visa given its name.
        /// </summary>
        /// <param name="nombre">The name of the desired Visa</param>
        /// <returns>The Visa if its the only object with that identifier, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Visa GetVisa(string name)
        {
            return _context.Visas.Where(v => v.Name == name).SingleOrDefault();
        }

        public Visa GetVisa(int id)
        {
            return _context.Visas.Where(v => v.VisaID == id).Single();
        }
    }
}
