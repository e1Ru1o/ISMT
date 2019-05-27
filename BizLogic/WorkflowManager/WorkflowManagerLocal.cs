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
            
            if (action == Action.Aprobar)
            {
                SaveHistorial(itinerario, itinerario.Usuario, usuario, Estado.AprobadoJefeArea, comentario);

                itinerario.Estado = Estado.PendienteAprobacionDecano;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);

                return;
            }

            if (action == Action.Rechazar)
            {
                itinerario.Estado = Estado.Pendiente;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
               
                return;
            }
        }

        public void ManageActionDecano(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;

            if (action == Action.Aprobar)
            {
                SaveHistorial(itinerario, itinerario.Usuario, usuario, Estado.AprobadoDecano, comentario);
               
                itinerario.Estado = Estado.PendienteAprobacionRector;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
                
                return;
            }

            if (action == Action.Rechazar)
            {
                itinerario.Estado = Estado.Pendiente;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);

                return;
            }
        }

        public void ManageActionRector(Itinerario itinerario, Action action, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;

            if (action == Action.Aprobar)
            {
                SaveHistorial(itinerario, itinerario.Usuario, usuario, Estado.AprobadoRector, comentario);

                itinerario.Estado = Estado.PendientePasaporte;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
               
                ManageActionPasaporte(itinerario.Usuario, Action.Ignorar, null, null);
                return;
            }

            if (action == Action.Rechazar)
            {
                itinerario.Estado = Estado.Pendiente;
                SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
              
                return;
            }
        }

        public void ManageActionPasaporte(Usuario usuarioItinerario, Action action, Usuario usuario, string comentario)
        {
            if (action == Action.Ignorar)
            {
                if (usuarioItinerario.HasPassport)
                {
                    SaveHistorial(null, usuarioItinerario, usuario, Estado.AprobadoPasaporte, comentario);
                  
                    foreach (var itinerario in usuarioItinerario.Itinerarios)
                        if (itinerario.Estado == Estado.PendientePasaporte)
                        {
                            itinerario.Update = 1;
                            itinerario.Estado = Estado.PendienteVisas;
                            SaveHistorial(itinerario, usuarioItinerario, usuario, itinerario.Estado, comentario);
                        }

                    ManageVisas(usuarioItinerario);
                }

                return;
            }

            if (action == Action.Aprobar)
            {
                SaveHistorial(null, usuarioItinerario, usuario, Estado.AprobadoPasaporte, comentario);

                foreach (var itinerario in usuarioItinerario.Itinerarios)
                    if (itinerario.Estado == Estado.PendientePasaporte)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.PendienteVisas;
                        SaveHistorial(itinerario, usuarioItinerario, usuario, itinerario.Estado, comentario);
                    }

                ManageVisas(usuarioItinerario);

                return;
            }

            if (action == Action.Rechazar)
            {
                SaveHistorial(null, usuarioItinerario, usuario, Estado.PendientePasaporte, comentario);
               
                foreach (var itinerario in usuarioItinerario.Itinerarios)
                    if (itinerario.Estado == Estado.PendientePasaporte)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.Pendiente;
                        SaveHistorial(itinerario, usuarioItinerario, usuario, itinerario.Estado, comentario);
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
                        SaveHistorial(itinerario, usuarioItinerario, usuario, itinerario.Estado, comentario);
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
                    SaveHistorial(itinerario, usuario, null, Estado.AprobadasVisas, null);
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
            if (action == Action.Aprobar)
            {
                SaveHistorial(null, usuarioTarget, usuario, Estado.AprobadasVisas, $"Aprobada visa {visa}");
                return;
            }

            if (action == Action.Rechazar)
            {
                SaveHistorial(null, usuarioTarget, usuario, Estado.Pendiente, comentario);
               
                foreach (var itinerario in usuarioTarget.Itinerarios)
                    if (itinerario.Estado == Estado.PendienteVisas)
                    {
                        itinerario.Update = 1;
                        itinerario.Estado = Estado.Pendiente;

                        SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
                    }
            }
        }

        public void CrearViaje(Itinerario itinerario, string claimTipoUsuario)
        {
            SaveHistorial(itinerario, itinerario.Usuario, null, Estado.Creado, null);

            itinerario.Update = 1;

            if (claimTipoUsuario == "Trabajador")
            {
                itinerario.Estado = Estado.PendienteAprobacionJefeArea;
                SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);

                return;
            }

            if (claimTipoUsuario == "JefeArea")
            {
                itinerario.Estado = Estado.PendienteAprobacionDecano;
                SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);

                return;
            }

            if (claimTipoUsuario == "Decano")
            {
                itinerario.Estado = Estado.PendienteAprobacionRector;
                SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);

                return;
            }

            if (claimTipoUsuario == "Rector")
            {
                itinerario.Estado = Estado.PendientePasaporte;
                SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);

                ManageActionPasaporte(itinerario.Usuario, Action.Ignorar, null, null);
                return;
            }
        }

        public void RealizarItinerario(Itinerario itinerario)
        {
            itinerario.Update = 1;
            itinerario.Estado = Estado.Realizado;

            SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);
        }

        public void CancelarItinerario(Itinerario itinerario, Usuario usuario, string comentario)
        {
            itinerario.Update = 1;
            itinerario.Estado = Estado.Cancelado;

            SaveHistorial(itinerario, itinerario.Usuario, usuario, itinerario.Estado, comentario);
        }

        public void ManageItinerarioPendiente(Itinerario itinerario)
        {
            var estado = itinerario.Historial.OrderBy(hist => hist.Fecha);
            itinerario.Estado = estado.ElementAt(estado.Count() - 2).Estado;
            itinerario.Update = 1;
            _context.Commit();

            SaveHistorial(itinerario, itinerario.Usuario, null, itinerario.Estado, null);
        }

        private void SaveHistorial(Itinerario itinerario, Usuario usuarioTarget, Usuario usuario, Estado estado, string comentario)
        {
            var historial_entity = new Historial
            {
                Estado = estado,
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
