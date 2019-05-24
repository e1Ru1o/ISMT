using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class ViajeInvitadoDbAccess : IEntityDbAccess<ViajeInvitado>
    {
        private readonly EfCoreContext _context;

        public ViajeInvitadoDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(ViajeInvitado entity)
        {
            _context.ViajesInvitados.Add(entity);
        }

        public void Delete(ViajeInvitado entity)
        {
            _context.ViajesInvitados.Remove(entity);
        }

        public IEnumerable<ViajeInvitado> GetAll() => _context.ViajesInvitados;

        public ViajeInvitado Update(ViajeInvitado entity, ViajeInvitado toUpd)
        {
            if (toUpd == null)
                throw new Exception("Viaje Invitado to be updated no exist");

            toUpd.FechaLLegada = entity.FechaLLegada ?? toUpd.FechaLLegada;
            toUpd.Motivo = entity.Motivo ?? toUpd.Motivo;
            toUpd.Nombre = entity.Nombre ?? toUpd.Motivo;
            toUpd.Procedencia = entity.Procedencia ?? toUpd.Procedencia;
            toUpd.Usuario = entity.Usuario ?? toUpd.Usuario;

            _context.ViajesInvitados.Update(toUpd);
            return toUpd;
        }

        public ViajeInvitado GetViajeInvitado(long id)
        {
            return _context.ViajesInvitados.Find(id);
        }

        public ViajeInvitado GetViajeInvitado(string nombre, string procedencia)
        {
            return _context.ViajesInvitados.Where(vi => vi.Nombre == nombre && vi.Procedencia == procedencia).SingleOrDefault();
        }
    }
}
