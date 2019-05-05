using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _context.Pasaportes.Add(entity);
        }

        public void Delete(Pasaporte entity)
        {
            _context.Pasaportes.Remove(entity);
        }

        public IEnumerable<Pasaporte> GetAll() => _context.Pasaportes;

        public Pasaporte Update(Pasaporte entity, Pasaporte toUpd)
        {
            if (toUpd == null)
                throw new Exception("Pasaporte to be updated no exist");

            toUpd.UsuarioCI = entity.UsuarioCI;
            toUpd.FechaCreacion = entity.FechaCreacion;
            toUpd.FechaVencimiento = entity.FechaVencimiento;
            toUpd.Actualizaciones = entity.Actualizaciones;
            toUpd.Tipo = entity.Tipo;
            toUpd.Usuario = entity.Usuario;
            toUpd.Visas = entity.Visas;

            _context.Pasaportes.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Passport given its code.
        /// </summary>
        /// <param name="codigo">The code of the desired Passport</param>
        /// <returns>The Passport if its the only object with that identifier, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Pasaporte GetPasaporte(string codigo)
        {
            return _context.Pasaportes.Where(p => p.CodigoPasaporte == codigo).SingleOrDefault();
        }
    }

}
