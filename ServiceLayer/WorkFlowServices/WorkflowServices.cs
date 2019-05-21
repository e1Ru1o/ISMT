using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using BizDbAccess.Utils;
using BizLogic.Workflow;
using BizLogic.Workflow.Concrete;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
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
        private readonly CiudadDbAccess _ciudadDbAccess;
        private readonly UserDbAccess _usuarioDbAccess;

        public WorkflowServices(IUnitOfWork context, UserManager<Usuario> userManager, GetterUtils getterUtils)
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
            _ciudadDbAccess = new CiudadDbAccess(_context);
            _usuarioDbAccess = new UserDbAccess(_context, null, userManager);
        }

       public async Task<int> RegisterItinerarioAsync(ItinerarioCommand cmd)
        {
            cmd.Usuario = await _userManager.FindByIdAsync(cmd.UsuarioID);

            var itinerario = _runnerItinerario.RunAction(cmd);

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

        public async Task<long> RegisterViajeAsync(ViajeCommand cmd)
        {
            cmd.Usuario = await _userManager.FindByIdAsync(cmd.UsuarioId);
            //cmd.Ciudad = _ciudadDbAccess.GetCiudad(cmd.CiudadName);
            //cmd.Institucion = _institucionDbAccess.GetInstitucion(cmd.InstitucionName);
            cmd.Pais = _paisDbAccess.GetPais(cmd.PaisName);

            try
            {
                cmd.Itinerario = _itinerarioDbAccess.GetItinerario(cmd.ItinerarioID);
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
            return _usuarioDbAccess.GetItinerariosNotFinished(usuario);
        }

        public IEnumerable<Itinerario> GetItinerarioDone(Usuario usuario)
        {
            return _usuarioDbAccess.GetItinerariosDone(usuario);
        }

        public IEnumerable<Itinerario> GetItinerarioCanceled(Usuario usuario)
        {
            return _usuarioDbAccess.GetItinerariosCanceled(usuario);
        }

        public IEnumerable<Itinerario> GetItinerariosEstado(Estado estado, Usuario user)
        {
            return _itinerarioDbAccess.GetItinerariosEstado(estado, user);
        }
    }
}
