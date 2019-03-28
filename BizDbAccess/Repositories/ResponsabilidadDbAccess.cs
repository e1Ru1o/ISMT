using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class ResponsabilidadDbAccess : IEntityDbAccess<Responsabilidad>
    {
        public readonly EfCoreContext _context;

        public ResponsabilidadDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Responsabilidad entity)
        {
            _context.Responsabilidades.Add(entity);
        }

        public void Delete(Responsabilidad entity)
        {
            _context.Responsabilidades.Remove(entity);
        }

        public IEnumerable<Responsabilidad> GetAll() => _context.Responsabilidades.OrderBy(i => i.Nombre);

        public Responsabilidad Update(Responsabilidad entity, Responsabilidad toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Responsabilidad updated not exist");

            toUpd.Nombre = entity.Nombre;
            toUpd.Usuarios = entity.Usuarios;

            _context.Responsabilidades.Update(toUpd);
            return toUpd;
        }

        public Responsabilidad GetResponsabilidad(string nombre)
        {
            return _context.Responsabilidades.Where(r => r.Nombre == nombre).SingleOrDefault();
        }
    }
}
