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
    public class WorkflowManager
    {
        private HistorialDbAccess _historial{ get; set; }

        public WorkflowManager(IUnitOfWork context)
        {
            _historial = new HistorialDbAccess(context);
        }

        public void Manage(Itinerario itinerario, Action action, Usuario usuario)
        {
            switch (itinerario.Estado)
            {
                case Estado.PendienteAprobacionJefeArea:
                    ManageActionJefeArea(itinerario, action, usuario);
                    break;

                case Estado.PendienteAprobacionDecano:
                    ManageActionDecano(itinerario, action, usuario);
                    break;

                case Estado.PendienteAprobacionRector:
                    if (ManageActionRector(itinerario, action, usuario))
                    {
                        if (ManageActionPasaporte(itinerario, Action.Ignorar, null))
                            ManageActionVisas(itinerario, Action.Ignorar, null);
                    }
                    break;

                case Estado.PendientePasaporte:
                    if (ManageActionPasaporte(itinerario, action, usuario))
                        ManageActionVisas(itinerario, Action.Ignorar, null);
                    break;

                case Estado.PendienteVisas:
                    ManageActionVisas(itinerario, action, usuario);
                    break;

                case Estado.PendienteRealizacion:
                    ManageActionRealizacion(itinerario, action);
                    break;
            }
        }

        private bool ManageActionJefeArea(Itinerario itinerario, Action action, Usuario usuario)
        {
            if (action == Action.Aprobar)
            {
                var historial_entity = new Historial
                {
                    Estado = Estado.AprobadoJefeArea,
                    Itinerario = itinerario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);

                itinerario.Estado = Estado.PendienteAprobacionDecano;
                return true;
            }

            return false;
        }

        private bool ManageActionDecano(Itinerario itinerario, Action action, Usuario usuario)
        {
            if(action == Action.Aprobar)
            {
                var historial_entity = new Historial
                {
                    Estado = Estado.AprobadoDecano,
                    Itinerario = itinerario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);

                itinerario.Estado = Estado.PendienteAprobacionRector;
                return true;
            }

            return false;
        }

        private bool ManageActionRector(Itinerario itinerario, Action action, Usuario usuario)
        {
            if (action == Action.Aprobar)
            {
                var historial_entity = new Historial
                {
                    Estado = Estado.AprobadoRector,
                    Itinerario = itinerario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);

                itinerario.Estado = Estado.PendientePasaporte;
                return true;
            }

            return false;
        }

        private bool ManageActionPasaporte(Itinerario itinerario, Action action, Usuario usuario)
        {
            if (action == Action.Ignorar)
            {
                if (itinerario.Usuario.HasPassport)
                {
                    var historial_entity = new Historial
                    {
                        Estado = Estado.AprobadoPasaporte,
                        Itinerario = itinerario,
                        Usuario = null,
                        Fecha = DateTime.Now
                    };
                    _historial.Add(historial_entity);

                    itinerario.Estado = Estado.PendienteVisas;
                    return true;
                }

                return false;
            }

            if (action == Action.Aprobar)
            {
                var historial_entity = new Historial
                {
                    Estado = Estado.AprobadoPasaporte,
                    Itinerario = itinerario,
                    Usuario = usuario,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);

                itinerario.Estado = Estado.PendienteVisas;
                return true;
            }

            return false;
        }

        private bool ManageActionVisas(Itinerario itinerario, Action action, Usuario usuario)
        {
            if (action == Action.Ignorar)
            {
                if (CurrentVisaPais(itinerario) is null)
                {
                    var historial_entity = new Historial
                    {
                        Estado = Estado.AprobadasVisas,
                        Itinerario = itinerario,
                        Usuario = null,
                        Fecha = DateTime.Now
                    };
                    _historial.Add(historial_entity);

                    itinerario.Estado = Estado.PendienteRealizacion;
                    return true;
                }

                return false;
            }

            if (action == Action.Aprobar)
            {
                if (CurrentVisaPais(itinerario) is null)
                {
                    var historial_entity = new Historial
                    {
                        Estado = Estado.AprobadasVisas,
                        Itinerario = itinerario,
                        Usuario = usuario,
                        Fecha = DateTime.Now
                    };
                    _historial.Add(historial_entity);

                    itinerario.Estado = Estado.PendienteRealizacion;
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool ManageActionRealizacion(Itinerario itinerario, Action action)
        {
            if (action == Action.Aprobar)
            {
                itinerario.Estado = Estado.Realizado;

                var historial_entity = new Historial
                {
                    Estado = itinerario.Estado,
                    Itinerario = itinerario,
                    Usuario = null,
                    Fecha = DateTime.Now
                };
                _historial.Add(historial_entity);

                return true;
            }

            return false;
        }

        public Pais CurrentVisaPais(Itinerario itinerario)
        {
            foreach (var viaje in itinerario.Viajes)
            {
                var visas = from visa in viaje.Pais.Visas
                            select visa.Visa;

                foreach (var visa in visas)
                    if (itinerario.Usuario.Visas.Contains(visa))
                        break;

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
                Usuario = null,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);

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
                itinerario.Estado = Estado.PendientePasaporte;
                return;
            }
        }

        public void CancelarItinerario(Itinerario itinerario, Usuario usuario)
        {
            itinerario.Estado = Estado.Cancelado;

            var historial_entity = new Historial
            {
                Estado = itinerario.Estado,
                Itinerario = itinerario,
                Usuario = usuario,
                Fecha = DateTime.Now
            };
            _historial.Add(historial_entity);
        }
    }
}
