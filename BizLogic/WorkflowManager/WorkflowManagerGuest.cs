using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using System;
using System.Collections.Generic;
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

        public void CrearViaje(Itinerario itinerario, string claimTipoUsuario)
        {
            var historial_entity = new Historial
            {
                Estado = Estado.Creado,
                Itinerario = itinerario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();

            if (claimTipoUsuario == "Trabajador")
            {
                itinerario.Estado = Estado.PendienteAprobacionJefeArea;
                return;
            }
            if (claimTipoUsuario == "JefeArea")
            {
                itinerario.Estado = Estado.PendienteAprobacionDecano;
                return;
            }
            if (claimTipoUsuario == "Decano")
            {
                itinerario.Estado = Estado.PendienteAprobacionDecano;
                return;
            }
            if (claimTipoUsuario == "Rector")
            {
                itinerario.Estado = Estado.PendienteRealizacion;
                return;
            }
        }

        public void ManageActionJefeArea(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadoJefeArea;
                _historial.Add(historial_entity);
                _context.Commit();

                itinerario.Estado = Estado.PendienteAprobacionDecano;
                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = itinerario.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionDecano(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadoDecano;
                _historial.Add(historial_entity);
                _context.Commit();

                itinerario.Estado = Estado.PendienteAprobacionRector;
                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = itinerario.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionRector(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Estado = Estado.AprobadoRector,
                Itinerario = itinerario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadoRector;
                _historial.Add(historial_entity);
                _context.Commit();

                itinerario.Estado = Estado.PendienteRealizacion;
                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = itinerario.Estado;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionRealizacion(Itinerario itinerario)
        {
            itinerario.Estado = Estado.Realizado;

            var historial_entity = new Historial
            {
                Estado = itinerario.Estado,
                Itinerario = itinerario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void CancelarItinerario(Itinerario itinerario, Usuario usuario, string comentario)
        {
            itinerario.Estado = Estado.Cancelado;

            var historial_entity = new Historial
            {
                Estado = itinerario.Estado,
                Itinerario = itinerario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }
    }
}
