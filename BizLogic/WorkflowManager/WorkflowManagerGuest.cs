using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizLogic.WorkflowManager
{
    public class WorkflowManagerGuest
    {
        private HistorialDbAccess _historial { get; set; }
        private IUnitOfWork _context { get; set; }

        public WorkflowManagerGuest(IUnitOfWork context)
        {
            _context = context;
            _historial = new HistorialDbAccess(_context);
        }

        public void CrearViaje(ViajeInvitado viajeInvitado, string claimTipoUsuario)
        {
            viajeInvitado.Update = 1;

            SaveHistorial(viajeInvitado, null, viajeInvitado.Estado, null);

            if (claimTipoUsuario == "Trabajador")
               viajeInvitado.Estado = Estado.PendienteAprobacionJefeArea;
            else if (claimTipoUsuario == "JefeArea")
               viajeInvitado.Estado = Estado.PendienteAprobacionDecano; 
            else if (claimTipoUsuario == "Decano")
                viajeInvitado.Estado = Estado.PendienteAprobacionRector;
            else
                viajeInvitado.Estado = Estado.PendienteRealizacion;

            SaveHistorial(viajeInvitado, null, viajeInvitado.Estado, null);
        }

        public void ManageActionJefeArea(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            viajeInvitado.Update = 1;
            
            if (action == Action.Aprobar)
            {
                SaveHistorial(viajeInvitado, usuario, Estado.AprobadoJefeArea, comentario);
                viajeInvitado.Estado = Estado.PendienteAprobacionDecano;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }
        }

        public void ManageActionDecano(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            viajeInvitado.Update = 1;

            if (action == Action.Aprobar)
            {
                SaveHistorial(viajeInvitado, usuario, Estado.AprobadoDecano, comentario);
                viajeInvitado.Estado = Estado.PendienteAprobacionRector;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }
        }

        public void ManageActionRector(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            viajeInvitado.Update = 1;

            if (action == Action.Aprobar)
            {
                SaveHistorial(viajeInvitado, usuario, Estado.AprobadoRector, comentario);
                viajeInvitado.Estado = Estado.PendienteRealizacion;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
                return;
            }
        }

        public void RealizarViajeInvitado(ViajeInvitado viajeInvitado)
        {
            viajeInvitado.Update = 1;
            viajeInvitado.Estado = Estado.Realizado;

            SaveHistorial(viajeInvitado, null, viajeInvitado.Estado, null);
        }

        public void CancelarViajeInvitado(ViajeInvitado viajeInvitado, Usuario usuario, string comentario)
        {
            viajeInvitado.Update = 1;
            viajeInvitado.Estado = Estado.Cancelado;
            _context.Commit();

            SaveHistorial(viajeInvitado, usuario, viajeInvitado.Estado, comentario);
        }

        public void ManageViajeInvitadoPendiente(ViajeInvitado viajeInvitado)
        {
            var estado = viajeInvitado.Historial.OrderBy(hist => hist.Fecha);
            viajeInvitado.Estado = estado.ElementAt(estado.Count() - 2).Estado;
            viajeInvitado.Update = 1;
            _context.Commit();

            SaveHistorial(viajeInvitado, null, viajeInvitado.Estado, null);
        }

        private void SaveHistorial(ViajeInvitado viajeInvitado, Usuario usuario, Estado estado, string comentario)
        {
            var historial_entity = new Historial
            {
                Estado = estado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };
            _historial.Add(historial_entity);
            _context.Commit();

        }
    }
}
