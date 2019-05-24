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
            itinerario.Update = 1;

            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now
            };

            if (action == Action.Aprobar)
            {
                itinerario.Estado = Estado.PendienteAprobacionDecano;
                historial_entity.Estado = Estado.AprobadoJefeArea;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = Estado.PendienteAprobacionDecano,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (action == Action.Rechazar)
            {
                itinerario.Estado = Estado.Pendiente;
                historial_entity.Estado = Estado.Pendiente;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionDecano(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;

            var historial_entity = new Historial
            {
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now
            };

            if (action == Action.Aprobar)
            {
                itinerario.Estado = Estado.PendienteAprobacionRector;
                historial_entity.Estado = Estado.AprobadoDecano;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = Estado.PendienteAprobacionRector,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (action == Action.Rechazar)
            {
                itinerario.Estado = Estado.Pendiente;
                historial_entity.Estado = Estado.Pendiente;
                _historial.Add(historial_entity);
                _context.Commit();
                return;
            }
        }

        public void ManageActionRector(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;

            var historial_entity = new Historial
            {
                Estado = Estado.AprobadoRector,
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };

            if (action == Action.Aprobar)
            {
                itinerario.Estado = Estado.PendientePasaporte;
                historial_entity.Estado = Estado.AprobadoRector;
                _historial.Add(historial_entity);
                _context.Commit();

                historial_entity = new Historial
                {
                    Estado = Estado.PendientePasaporte,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Usuario = usuario,
                    Fecha = DateTime.Now,
                    Comentario = comentario
                };
                _historial.Add(historial_entity);
                _context.Commit();

                ManageActionPasaporte(itinerario.Usuario, Action.Ignorar, null, null);
                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = itinerario.Estado;
                _historial.Add(historial_entity);
                itinerario.Estado = Estado.Pendiente;
                _context.Commit();
                return;
            }
        }

        public void ManageActionPasaporte(Usuario usuarioItinerario, Action action, Usuario usuario, string comentario)
        {
            var historial_entity = new Historial
            {
                Fecha = DateTime.Now,
                UsuarioTarget = usuarioItinerario
            };

            if (action == Action.Ignorar)
            {
                if (usuarioItinerario.HasPassport)
                {
                    historial_entity.Estado = Estado.AprobadoPasaporte;
                    _historial.Add(historial_entity);
                    _context.Commit();

                    foreach (var itinerario in usuarioItinerario.Itinerarios)
                        if (itinerario.Estado == Estado.PendientePasaporte)
                        {
                            itinerario.Update = 1;
                            itinerario.Estado = Estado.PendienteVisas;

                            historial_entity = new Historial
                            {
                                Estado = Estado.PendienteVisas,
                                Itinerario = itinerario,
                                UsuarioTarget = usuarioItinerario,
                                Fecha = DateTime.Now,
                            };
                            _historial.Add(historial_entity);
                            _context.Commit();
                        }

                    ManageVisas(usuarioItinerario);
                }

                return;
            }

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadoPasaporte;
                historial_entity.Usuario = usuario;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);
                _context.Commit();

                foreach (var itinerario in usuarioItinerario.Itinerarios)
                    if (itinerario.Estado == Estado.PendientePasaporte)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.PendienteVisas;

                        historial_entity = new Historial
                        {
                            Estado = Estado.PendienteVisas,
                            Itinerario = itinerario,
                            UsuarioTarget = usuarioItinerario,
                            Usuario = usuario,
                            Fecha = DateTime.Now,
                        };
                        _historial.Add(historial_entity);
                        _context.Commit();
                    }
                ManageVisas(usuarioItinerario);

                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = Estado.PendientePasaporte;
                historial_entity.Usuario = usuario;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);

                foreach (var itinerario in usuarioItinerario.Itinerarios)
                    if (itinerario.Estado == Estado.PendientePasaporte)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.Pendiente;

                        historial_entity = new Historial
                        {
                            Estado = itinerario.Estado,
                            Itinerario = itinerario,
                            UsuarioTarget = usuarioItinerario,
                            Usuario = usuario,
                            Fecha = DateTime.Now,
                        };
                        _historial.Add(historial_entity);
                        _context.Commit();
                    }

                return;
            }

            if (action == Action.Cancelar)
            {
                foreach (var itinerario in usuarioItinerario.Itinerarios)
                    if (itinerario.Estado == Estado.PendientePasaporte)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.Cancelado;

                        historial_entity = new Historial
                        {
                            Estado = itinerario.Estado,
                            Itinerario = itinerario,
                            UsuarioTarget = usuarioItinerario,
                            Usuario = usuario,
                            Fecha = DateTime.Now,
                        };
                        _historial.Add(historial_entity);
                        _context.Commit();
                    }
            }
        }

        public IEnumerable<Visa> ManageVisas(Usuario usuario)
        {
            var visas_usuario = from visa in usuario.Visas
                                select visa.Visa;

            var itinerarios = usuario.Itinerarios.Where(it => it.Estado == Estado.PendienteVisas);
            var visas = new HashSet<Visa>();

            foreach (var itinerario in itinerarios)
            {
                var currentPais = CurrentPaisItinerario(itinerario, visas_usuario);

                if (currentPais is null)
                {
                    itinerario.Update = 1;
                    itinerario.Estado = Estado.PendienteRealizacion;

                    var historial_entity = new Historial()
                    {
                        Itinerario = itinerario,
                        Estado = Estado.AprobadasVisas,
                        UsuarioTarget = usuario,
                        Fecha = DateTime.Now
                    };
                    _historial.Add(historial_entity);
                    _context.Commit();
                }
                else
                {
                    foreach (var visa in GetVisasPais(currentPais))
                        visas.Add(visa);
                }
            }

            return visas;
        }

        private Pais CurrentPaisItinerario(Itinerario itinerario, IEnumerable<Visa> visas_usuario)
        {
            bool change = false;

            foreach (var viaje in itinerario.Viajes)
            {
                change = false;
                var visas = GetVisasPais(viaje.Pais);

                if (visas.Count() != 0)
                {
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
                else
                    continue;
            }

            return null;
        }

        private IEnumerable<Visa> GetVisasPais(Pais pais)
        {
            var visas_pais = from visa in pais.Visas
                             select visa.Visa;
            var visas_region = from visa in pais.Region.Visas
                               select visa.Visa;

            IEnumerable<Visa> visas;
            if (visas_region is null && visas_pais is null)
                visas = new List<Visa>();
            else if (visas_pais is null)
                visas = visas_region;
            else if (visas_region is null)
                visas = visas_pais;
            else
                visas = visas_pais.Concat(visas_region);

            return visas;
        }

        public void ManageActionVisas(Usuario usuarioTarget, Action action, Usuario usuario, string visa, string comentario)
        {
            var historial_entity = new Historial()
            {
                UsuarioTarget = usuarioTarget,
                Usuario = usuario,
                Fecha = DateTime.Now
            };

            if (action == Action.Aprobar)
            {
                historial_entity.Estado = Estado.AprobadasVisas;
                historial_entity.Comentario = $"Aprobada visa {visa}";

                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (action == Action.Rechazar)
            {
                historial_entity.Estado = Estado.Pendiente;
                historial_entity.Comentario = comentario;
                _historial.Add(historial_entity);
                _context.Commit();

                foreach (var itinerario in usuarioTarget.Itinerarios)
                    if (itinerario.Estado == Estado.PendienteVisas)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.Pendiente;

                        historial_entity = new Historial()
                        {
                            Estado = itinerario.Estado,
                            Itinerario = itinerario,
                            UsuarioTarget = usuarioTarget,
                            Usuario = usuario,
                            Fecha = DateTime.Now,
                            Comentario = comentario 
                        };
                        _historial.Add(historial_entity);
                        _context.Commit();
                    }
            }
        }

        public void CrearViaje(Itinerario itinerario, string claimTipoUsuario)
        {
            var historial_entity = new Historial
            {
                Estado = Estado.Creado,
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();

            itinerario.Update = 1;

            if (claimTipoUsuario == "Trabajador")
            {
                itinerario.Estado = Estado.PendienteAprobacionJefeArea;

                historial_entity = new Historial
                {
                    Estado = Estado.PendienteAprobacionJefeArea,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (claimTipoUsuario == "JefeArea")
            {
                itinerario.Estado = Estado.PendienteAprobacionDecano;

                historial_entity = new Historial
                {
                    Estado = Estado.PendienteAprobacionDecano,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (claimTipoUsuario == "Decano")
            {
                itinerario.Estado = Estado.PendienteAprobacionRector;

                historial_entity = new Historial
                {
                    Estado = Estado.PendienteAprobacionRector,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                return;
            }

            if (claimTipoUsuario == "Rector")
            {
                itinerario.Estado = Estado.PendientePasaporte;

                historial_entity = new Historial
                {
                    Estado = Estado.PendientePasaporte,
                    Itinerario = itinerario,
                    UsuarioTarget = itinerario.Usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);
                _context.Commit();

                ManageActionPasaporte(itinerario.Usuario, Action.Ignorar, null, null);
                return;
            }
        }

        public void RealizarItinerario(Itinerario itinerario)
        {
            itinerario.Update = 1;
            itinerario.Estado = Estado.Realizado;

            var historial_entity = new Historial
            {
                Estado = itinerario.Estado,
                UsuarioTarget = itinerario.Usuario,
                Usuario = itinerario.Usuario,
                Itinerario = itinerario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void CancelarItinerario(Itinerario itinerario, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;
            itinerario.Estado = Estado.Cancelado;

            var historial_entity = new Historial
            {
                Estado = itinerario.Estado,
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Usuario = usuario,
                Fecha = DateTime.Now,
                Comentario = comentario
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }

        public void ManageItinerarioPendiente(Itinerario itinerario)
        {
            var estado = itinerario.Historial.OrderBy(hist => hist.Fecha);
            itinerario.Estado = estado.ElementAt(estado.Count() - 2).Estado;
            itinerario.Update = 1;
            _context.Commit();

            var historial_entity = new Historial()
            {
                Estado = itinerario.Estado,
                Itinerario = itinerario,
                UsuarioTarget = itinerario.Usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
            _context.Commit();
        }
    }
}
