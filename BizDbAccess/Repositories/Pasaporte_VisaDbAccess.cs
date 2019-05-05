using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class Pasaporte_VisaDbAccess : IEntityDbAccess<Pasaporte_Visa>
    {
        private readonly EfCoreContext _context;

        public Pasaporte_VisaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Pasaporte_Visa entity)
        {
            _context.Pasaportes_Visas.Add(entity);
        }

        public void Delete(Pasaporte_Visa entity)
        {
            _context.Pasaportes_Visas.Remove(entity);
        }

        public IEnumerable<Pasaporte_Visa> GetAll() => _context.Pasaportes_Visas;

        public Pasaporte_Visa Update(Pasaporte_Visa entity, Pasaporte_Visa toUpd)
        {
            if (toUpd == null)
                throw new Exception("Pasaporte_Visa to be updated no exist");

            toUpd.Pasaporte = entity.Pasaporte ?? toUpd.Pasaporte;
            toUpd.Visa = entity.Visa ?? toUpd.Visa;

            _context.Pasaportes_Visas.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Pasaporte_Visa given its pasaporte code and visa name.
        /// </summary>
        /// <param name="pasaporteCode">The code of the pasaporte of the desired pasaporte.</param>
        /// <param name="visaName">The name of the visa of the desired object.</param>
        /// <returns>The Pasaporte_Visa if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Pasaporte_Visa GetPasaporte_Visa(string pasaporteCode, string visaName)
        {
            return _context.Pasaportes_Visas.Where(pv => pv.Pasaporte.CodigoPasaporte == pasaporteCode &&
                                                         pv.Visa.Name == visaName).SingleOrDefault();
        }
    }
}
