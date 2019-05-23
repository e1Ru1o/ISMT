using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class Pais_VisaDbAccess : IEntityDbAccess<Pais_Visa>
    {
        private readonly EfCoreContext _context;

        public Pais_VisaDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Pais_Visa entity)
        {
            _context.Paises_Visas.Add(entity);
        }

        public void Delete(Pais_Visa entity)
        {
            _context.Paises_Visas.Remove(entity);
        }

        public IEnumerable<Pais_Visa> GetAll() => _context.Paises_Visas;

        public Pais_Visa Update(Pais_Visa entity, Pais_Visa toUpd)
        {
            if (toUpd == null)
                throw new Exception("Pais_Visa to be updated no exist");

            toUpd.Pais = entity.Pais ?? toUpd.Pais;
            toUpd.Visa = entity.Visa ?? toUpd.Visa;

            _context.Paises_Visas.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Pais_Visa given its country name and visa name.
        /// </summary>
        /// <param name="paisName">The name of the country of the desired object.</param>
        /// <param name="visaName">The name of the visa of the desired object.</param>
        /// <returns>The Pais_Visa if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Pais_Visa GetPais_Visa(string paisName, string visaName)
        {
            return _context.Paises_Visas.Where(pv => pv.Pais.Nombre == paisName &&
                                                     pv.Visa.Name == visaName).SingleOrDefault();
        }
    }
}
