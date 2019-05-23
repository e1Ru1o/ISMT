using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using BizDbAccess.Repositories;

namespace BizLogic.WorkflowManager
{
    public class WorkflowManagerLocal
    {
        private HistorialDbAccess _historial { get; set; }
        private IUnitOfWork _context { get; set; }

        public WorkflowManagerLocal(IUnitOfWork context)
        {
            _context = context;
            _historial = new HistorialDbAccess(_context);
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

                itinerario.Estado = Estado.PendientePasaporte;
                ManageActionPasaporte(itinerario, Action.Ignorar, null, null);
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

        public void ManageActionPasaporte(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                Fecha = DateTime.Now
            };

            if (action == Action.Ignorar)
            {
                if (itinerario.Usuario.HasPassport)
                {
                    historial_entity.Estado = Estado.AprobadoPasaporte;
                    historial_entity.Itinerario = itinerario;
                    _historial.Add(historial_entity);
                    _context.Commit();

                    itinerario.Estado = Estado.PendienteVisas;
                    ManageActionVisas(itinerario, Action.Ignorar, null, null);
                }

                return;
            }

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadoPasaporte;
                historial_entity.Itinerario = itinerario;
                historial_entity.Usuario = usuario;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);
                _context.Commit();

                itinerario.Estado = Estado.PendienteVisas;
                ManageActionVisas(itinerario, Action.Ignorar, null, null);
                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = itinerario.Estado;
                historial_entity.Usuario = usuario;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionVisas(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                Fecha = DateTime.Now
            };

            if (action == Action.Ignorar)
            {
                Pais pais = CurrentVisaItinerario(itinerario);

                if (pais is null)
                {
                    historial_entity.Estado = Estado.AprobadasVisas;
                    _historial.Add(historial_entity);
                    _context.Commit();

                    itinerario.Estado = Estado.PendienteRealizacion;
                }

                return;
            }

            if (action == Action.Aprobar)
            {
                Pais pais = CurrentVisaItinerario(itinerario);

                if (pais is null)
                {
                    historial_entity.Estado = Estado.AprobadasVisas;
                    historial_entity.Usuario = usuario;
                    historial_entity.Comentario = comentario;
                    _historial.Add(historial_entity);
                    _context.Commit();

                    itinerario.Estado = Estado.PendienteRealizacion;
                }

                return;
            }

            else
            {
                historial_entity.Estado = itinerario.Estado;
                historial_entity.Usuario = usuario;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);
                _context.Commit();

                return ;
            }
        }

        public Pais CurrentVisaItinerario(Itinerario itinerario)
        {
            bool change = false;
            var visas_usuario = from visa in itinerario.Usuario.Visas
                                select visa.Visa;
            
            foreach (var viaje in itinerario.Viajes)
            {
                change = false;
                var visas_pais = from visa in viaje.Pais.Visas
                                 select visa.Visa;
                var visas_region = from visa in viaje.Pais.Region.Visas
                                   select visa.Visa;
                                    
                IEnumerable<Visa> visas;
                if (visas_region is null && visas_pais is null)
                    continue;
                else if (visas_pais is null)
                    visas = visas_region;
                else if (visas_region is null)
                    visas = visas_pais;
                else
                    visas = visas_pais.Concat(visas_region);

                foreach (var visa in visas)
                    if (visas_usuario.Contains(visa))
                    {
                        change = true;
                        break;
                    }

                if (change)
                    continue;

                return viaje.Pais;
            }

            return null;
        }

        public void CrearViaje(Itinerario itinerario, string claimTipoUsuario)
        {
            var historial_entity = new Historial
            {
                Estado = Estado.Creado,
                Itinerario = itinerario,
                Usuario = itinerario.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);

            if (claimTipoUsuario == "Trabajador")
                itinerario.Estado = Estado.PendienteAprobacionJefeArea;
            else if (claimTipoUsuario == "JefeArea")
                itinerario.Estado = Estado.PendienteAprobacionDecano;
            else if (claimTipoUsuario == "Decano")
                itinerario.Estado = Estado.PendienteAprobacionDecano;
            else
            {
                itinerario.Estado = Estado.PendientePasaporte;
                ManageActionPasaporte(itinerario, Action.Ignorar, null, null);
            }

            _context.Commit();
        }

        public void RealizarItinerario(Itinerario itinerario)
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
