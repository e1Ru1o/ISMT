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
            var historial_entity = new Historial
            {
                Estado = Estado.Creado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();

            if (claimTipoUsuario == "Trabajador")
               viajeInvitado.Estado = Estado.PendienteAprobacionJefeArea;
            else if (claimTipoUsuario == "JefeArea")
               viajeInvitado.Estado = Estado.PendienteAprobacionDecano; 
            else if (claimTipoUsuario == "Decano")
                viajeInvitado.Estado = Estado.PendienteAprobacionDecano;
            else
                viajeInvitado.Estado = Estado.PendienteRealizacion;

            historial_entity = new Historial
            {
                Estado = viajeInvitado.Estado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void ManageActionJefeArea(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now
            };

            if (action == Action.Aprobar)
            {
                viajeInvitado.Estado = Estado.PendienteAprobacionDecano;
                historial_entity.Estado = Estado.AprobadoJefeArea;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = viajeInvitado.Estado,
                    ViajeInvitado = viajeInvitado,
                    UsuarioTarget = viajeInvitado.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                historial_entity.Estado = viajeInvitado.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionDecano(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                viajeInvitado.Estado = Estado.PendienteAprobacionRector;
                historial_entity.Estado = Estado.AprobadoDecano;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = viajeInvitado.Estado,
                    ViajeInvitado = viajeInvitado,
                    UsuarioTarget = viajeInvitado.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                historial_entity.Estado = viajeInvitado.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionRector(ViajeInvitado viajeInvitado, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Estado = Estado.AprobadoRector,
                ViajeInvitado = viajeInvitado,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                viajeInvitado.Estado = Estado.PendienteRealizacion;
                historial_entity.Estado = Estado.AprobadoRector;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = viajeInvitado.Estado,
                    ViajeInvitado = viajeInvitado,
                    UsuarioTarget = viajeInvitado.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (action == Action.Rechazar)
            {
                viajeInvitado.Estado = Estado.Pendiente;
                historial_entity.Estado = viajeInvitado.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void RealizarViajeInvitado(ViajeInvitado viajeInvitado)
        {
            viajeInvitado.Estado = Estado.Realizado;

            var historial_entity = new Historial
            {
                Estado = viajeInvitado.Estado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void CancelarViajeInvitado(ViajeInvitado viajeInvitado, Usuario usuario, string comentario)
        {
            viajeInvitado.Estado = Estado.Cancelado;

            var historial_entity = new Historial
            {
                Estado = viajeInvitado.Estado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void ManageViajeInvitadoPendiente(ViajeInvitado viajeInvitado)
        {
            var estado = viajeInvitado.Historial.OrderBy(hist => hist.Fecha);
            viajeInvitado.Estado = estado.ElementAt(estado.Count() - 2).Estado;
            _context.Commit();

            var historial_entity = new Historial()
            {
                Estado = viajeInvitado.Estado,
                ViajeInvitado = viajeInvitado,
                UsuarioTarget = viajeInvitado.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }
    }
}
