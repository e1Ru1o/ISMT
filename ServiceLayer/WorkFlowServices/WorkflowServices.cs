using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using BizDbAccess.Utils;
using BizLogic.Workflow;
using BizLogic.Workflow.Concrete;
using BizLogic.WorkflowManager;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.WorkFlowServices
{
    public class WorkflowServices
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly GetterUtils _getterUtils;

        private readonly RunnerWriteDb<ItinerarioCommand, Itinerario> _runnerItinerario;
        private readonly RunnerWriteDb<ViajeCommand, Viaje> _runnerViaje;

        private readonly ItinerarioDbAccess _itinerarioDbAccess;
        private readonly ViajeDbAccess _viajeDbAccess;
        private readonly PaisDbAccess _paisDbAccess;
        private readonly InstitucionDbAccess _institucionDbAccess;
        private readonly UserDbAccess _userDbAccess;
        private readonly VisaDbAccess _visaDbAccess;

        private readonly WorkflowManagerLocal _workflowManagerLocal;

        public WorkflowServices(IUnitOfWork context, UserManager<Usuario> userManager, GetterUtils getterUtils, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _getterUtils = getterUtils;

            _runnerItinerario = new RunnerWriteDb<ItinerarioCommand, Itinerario>(
                new RegisterItinerarioAction(new ItinerarioDbAccess(_context)), _context);
            _runnerViaje = new RunnerWriteDb<ViajeCommand, Viaje>(
                new RegisterViajeAction(new ViajeDbAccess(_context)), _context);

            _itinerarioDbAccess = new ItinerarioDbAccess(_context);
            _viajeDbAccess = new ViajeDbAccess(_context);
            _paisDbAccess = new PaisDbAccess(_context);
            _institucionDbAccess = new InstitucionDbAccess(_context);
            _userDbAccess = new UserDbAccess(_context, signInManager, userManager);
            _workflowManagerLocal = new WorkflowManagerLocal(context);
            _visaDbAccess = new VisaDbAccess(context);
        }

       public int RegisterItinerarioAsync(ItinerarioCommand cmd, string claimTipoInstitucion)
        {
            var iters = _userDbAccess.GetAllItinerarios();
            cmd.Usuario = _userDbAccess.GetUsuario(cmd.UsuarioID); //await _userManager.FindByIdAsync(cmd.UsuarioID);
            var itinerario = _runnerItinerario.RunAction(cmd);

            _workflowManagerLocal.CrearViaje(itinerario, claimTipoInstitucion);

            return itinerario.ItinerarioID;
        }

        public Itinerario UpdateItinerario(Itinerario entity, Itinerario toUpd)
        {
            var itinerario = _itinerarioDbAccess.Update(entity, toUpd);
            _context.Commit();
            return itinerario;
        }

        public void Removeitinerario(Itinerario entity)
        {
            _itinerarioDbAccess.Delete(entity);
            _context.Commit();
        }

        public Itinerario GetItinerario(int id)
        {
            return _itinerarioDbAccess.GetItinerario(id);
        }

        public void CalculateDates(Itinerario iter)
        {
            var initials = iter.Viajes.Select(v => v.FechaInicio).ToList();
            initials.Sort();
            iter.FechaInicio = initials.First();

            var finals = iter.Viajes.Select(v => v.FechaFin).ToList();
            finals.Sort();
            iter.FechaFin = finals.First();

            _context.Commit();
        }

        public long RegisterViajeAsync(ViajeCommand cmd)
        {
            //cmd.Institucion = _institucionDbAccess.GetInstitucion(cmd.InstitucionName);
            cmd.Pais = _paisDbAccess.GetPais(cmd.PaisName);

            try
            {
                cmd.Itinerario = _userDbAccess.GetItinerario(cmd.UsuarioId, cmd.ItinerarioID);
            }
            catch (InvalidOperationException)
            {
                return -1;
            }

            var viaje = _runnerViaje.RunAction(cmd);

            return viaje.ViajeID;
        }

        public IEnumerable<Itinerario> GetItinerarioNotFinished(Usuario usuario)
        {
            return _userDbAccess.GetItinerariosNotFinished(usuario);
        }

        public IEnumerable<Itinerario> GetItinerarioDone(Usuario usuario)
        {
            return _userDbAccess.GetItinerariosDone(usuario);
        }

        public IEnumerable<Itinerario> GetItinerarioCanceled(Usuario usuario)
        {
            return _userDbAccess.GetItinerariosCanceled(usuario);
        }

        public IEnumerable<Itinerario> GetItinerariosEstado(Estado estado, Usuario user)
        {
            return _itinerarioDbAccess.GetItinerariosEstado(estado, user);
        }

        public void ManageActionAprobar(int itinerarioId, string usuarioId, string comentario)
        {
            var itinerario = _itinerarioDbAccess.GetItinerario(itinerarioId);
            var usuario = _userDbAccess.GetUsuario(usuarioId);

            if(itinerario.Estado == Estado.PendienteAprobacionJefeArea)
            {
                _workflowManagerLocal.ManageActionJefeArea(itinerario, BizLogic.WorkflowManager.Action.Aprobar, usuario, comentario);
                return;
            }

            if (itinerario.Estado == Estado.PendienteAprobacionDecano)
            {
                _workflowManagerLocal.ManageActionDecano(itinerario, BizLogic.WorkflowManager.Action.Aprobar, usuario, comentario);
                return;
            }

            if (itinerario.Estado == Estado.PendienteAprobacionRector)
            {
                _workflowManagerLocal.ManageActionRector(itinerario, BizLogic.WorkflowManager.Action.Aprobar, usuario, comentario);
                return;
            }
        }
        
        public void ManageActionRechazar(int itinerarioId, string usuarioId, string comentario)
        {
            var itinerario = _itinerarioDbAccess.GetItinerario(itinerarioId);
            var usuario = _userDbAccess.GetUsuario(usuarioId);

            if (itinerario.Estado == Estado.PendienteAprobacionJefeArea)
            {
                _workflowManagerLocal.ManageActionJefeArea(itinerario, BizLogic.WorkflowManager.Action.Rechazar, usuario, comentario);
                return;
            }

            if (itinerario.Estado == Estado.PendienteAprobacionDecano)
            {
                _workflowManagerLocal.ManageActionDecano(itinerario, BizLogic.WorkflowManager.Action.Rechazar, usuario, comentario);
                return;
            }

            if (itinerario.Estado == Estado.PendienteAprobacionRector)
            {
                _workflowManagerLocal.ManageActionRector(itinerario, BizLogic.WorkflowManager.Action.Rechazar, usuario, comentario);
                return;
            }
        }
        
        public void RealizarItinerario(int itinerarioId)
        {
            var itinerario = _itinerarioDbAccess.GetItinerario(itinerarioId);
            _workflowManagerLocal.RealizarItinerario(itinerario);
        }

        public void CancelItinerario(int itinerarioId, string usuarioId, string comentario)
        {
            var trip = _itinerarioDbAccess.GetItinerario(itinerarioId);
            var usuario = _userDbAccess.GetUsuario(usuarioId);
            _workflowManagerLocal.CancelarItinerario(trip, usuario, comentario);
        }

        public async void SetPassportToUser(int iterID, string userToUpdID, string updatorID, string comment)
        {
            var iter = _itinerarioDbAccess.GetItinerario(iterID);
            var userToUpd = await _userManager.FindByIdAsync(userToUpdID);
            var updator = await _userManager.FindByIdAsync(updatorID);

            userToUpd.HasPassport = true;
            await _userManager.UpdateAsync(userToUpd);
            _context.Commit();
        }

        public async void SetVisaToUser(int visaID, string userToUpdID, string updatorID)
        {
            var visa = _visaDbAccess.GetVisa(visaID);
            var userToUpd = await _userManager.FindByIdAsync(userToUpdID);
            var updator = await _userManager.FindByIdAsync(updatorID);

            var user_visa = new Usuario_Visa()
            {
                Usuario = userToUpd,
                Visa = visa
            };

            if (userToUpd.Visas == null)
                userToUpd.Visas = new List<Usuario_Visa>();
            if (visa.Usuarios == null)
                visa.Usuarios = new List<Usuario_Visa>();

            userToUpd.Visas.Add(user_visa);
            visa.Usuarios.Add(user_visa);

            await _userManager.UpdateAsync(userToUpd);
            visa = _visaDbAccess.Update(visa, _visaDbAccess.GetVisa(visaID));
        }

    }
}
