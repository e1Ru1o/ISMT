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
    }

}
