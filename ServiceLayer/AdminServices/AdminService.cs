using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using BizDbAccess.Utils;
using BizLogic.Administration;
using BizLogic.Administration.Concrete;
using BizLogic.Shared;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices
{
    public class AdminService
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly GetterUtils _getterUtils;

        private readonly RunnerWriteDb<NameOnlyViewModel, Institucion> _runnerInstitucion;
        private readonly RunnerWriteDb<PaisCommand, Pais> _runnerPais;
        private readonly RunnerWriteDb<VisaCommand, Visa> _runnerVisa;
        private readonly RunnerWriteDb<NameOnlyViewModel, Region> _runnerRegion;

        private readonly PaisDbAccess _paisDbAccess;
        private readonly InstitucionDbAccess _institucionDbAccess;
        private readonly Pais_VisaDbAccess _pais_VisaDbAccess;
        private readonly VisaDbAccess _visaDbAccess;
        private readonly RegionDbAccess _regionDbAccess;

        public AdminService(IUnitOfWork context, UserManager<Usuario> userManager, GetterUtils getterUtils)
        {
            _context = context;
            _userManager = userManager;
            _getterUtils = getterUtils;

            _runnerInstitucion = new RunnerWriteDb<NameOnlyViewModel, Institucion>(
                new RegisterInstitucionAction(new InstitucionDbAccess(_context)), _context);
            _runnerPais = new RunnerWriteDb<PaisCommand, Pais>(
                new RegisterPaisAction(new PaisDbAccess(_context)), _context);
            _runnerVisa = new RunnerWriteDb<VisaCommand, Visa>(
                new RegisterVisaAction(new VisaDbAccess(_context)), _context);
            _runnerRegion = new RunnerWriteDb<NameOnlyViewModel, Region>(
                new RegisterRegionAction(new RegionDbAccess(_context)), _context);

            _paisDbAccess = new PaisDbAccess(_context);
            _institucionDbAccess = new InstitucionDbAccess(_context);
            _pais_VisaDbAccess = new Pais_VisaDbAccess(_context);
            _visaDbAccess = new VisaDbAccess(_context);
            _regionDbAccess = new RegionDbAccess(_context);
        }

        public long RegisterInstitucion(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            var institucion = _runnerInstitucion.RunAction(vm);

            if (_runnerInstitucion.HasErrors)
            {
                errors = _runnerInstitucion.Errors;
                return -1;
            }

            errors = null;
            return institucion.InstitucionID;
        }

        public Institucion UpdateInstitucion(Institucion entity, Institucion toUpd)
        {
            var institucion = _institucionDbAccess.Update(entity, toUpd);
            _context.Commit();
            return institucion;
        }

        public void RemoveInstitucion(Institucion entity)
        {
            _institucionDbAccess.Delete(entity);
            _context.Commit();
        }

        public long RegisterPais(PaisCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            if (cmd.RegionName != null)
                cmd.Region = _regionDbAccess.GetRegion(cmd.RegionName);

            var pais = _runnerPais.RunAction(cmd);

            if (_runnerPais.HasErrors)
            {
                errors = _runnerPais.Errors;
                return -1;
            }

            errors = null;
            return pais.PaisID;
        }

        public Pais UpdatePais(Pais entity, Pais toUpd)
        {
            var pais = _paisDbAccess.Update(entity, toUpd);
            _context.Commit();
            return pais;
        }

        public void RemovePais(Pais entity)
        {
            _paisDbAccess.Delete(entity);
            _context.Commit();
        }

        public IEnumerable<Pais> GetPaisesWithoutVisa(string visaName)
        {
            var paises_visas = _pais_VisaDbAccess.GetAll().Where(pv => pv.Visa.Name != visaName).Select(pv => pv.Pais);
            var paises = _paisDbAccess.GetAll();

            foreach (var p in paises)
            {
                if (!paises_visas.Where(pv => pv.Nombre == p.Nombre).Any())
                    yield return p;
            }
        }

        public IEnumerable<Pais_Visa> BuildListOfPais_Visa(IEnumerable<Pais> paises, IEnumerable<Visa> visas)
        {
            foreach (var v in visas)
                foreach (var p in paises)
                    yield return new Pais_Visa()
                    {
                        Pais = p,
                        Visa = v
                    };
        }

        public IEnumerable<Region_Visa> BuildListOfRegion_Visa(IEnumerable<Region> regiones, IEnumerable<Visa> visas)
        {
            foreach (var v in visas)
                foreach (var r in regiones)
                    yield return new Region_Visa()
                    {
                        Region = r,
                        Visa = v
                    };
        }

        public long RegisterVisa(VisaCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            if (cmd.regionesName != null)
            {
                var aux = new List<Region>();
                foreach (var name in cmd.regionesName)
                    foreach (var region in _regionDbAccess.GetAll())
                    {
                        if (region.Nombre == name)
                            aux.Add(region);
                    }

                cmd.Regiones = aux;
                cmd.RegionesVisas = BuildListOfRegion_Visa(cmd.Regiones, new List<Visa>() { new Visa() { Name = cmd.Nombre } });
            }

            if (cmd.paisesNames != null)
            {
                var aux = new List<Pais>();
                foreach (var name in cmd.paisesNames)
                    foreach (var pais in _paisDbAccess.GetAll())
                    {
                        if (pais.Nombre == name)
                            aux.Add(pais);
                    }

                cmd.Paises = aux;
                cmd.PaisesVisas = BuildListOfPais_Visa(cmd.Paises, new List<Visa>() { new Visa() { Name = cmd.Nombre } });
            }

            var visa =  _runnerVisa.RunAction(cmd);

            if (_runnerVisa.HasErrors)
            {
                errors = _runnerVisa.Errors;
                return -1;
            }
    
            errors = null;
            return visa.VisaID;
        }

        public Visa UpdateVisa(Visa entity, Visa toUpd)
        {
            var visa = _visaDbAccess.Update(entity, toUpd);
            _context.Commit();
            return visa;
        }

        public void RemoveVisa(Visa entity)
        {
            _visaDbAccess.Delete(entity);
            _context.Commit();
        }

        public long RegisterRegion(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            var region = _runnerRegion.RunAction(vm);

            if (_runnerPais.HasErrors)
            {
                errors = _runnerPais.Errors;
                return -1;
            }

            errors = null;
            return region.RegionID;
        }

        public Region UpdateRegion(Region entity, Region toUpd)
        {
            var region = _regionDbAccess.Update(entity, toUpd);
            _context.Commit();
            return region;
        }

        public void RemoveRegion(Region entity)
        {
            _regionDbAccess.Delete(entity);
            _context.Commit();
        }

        public async Task<(List<string> UserPendings, List<Itinerario> ViajesUpdated, List<ViajeInvitado> InvitadosUpdated)> FillNotificationsAsync()
        {
            //check for pending users
            List<string> UserPendings = new List<string>();
            foreach (var user in _userManager.Users)
            {
                if ((await _userManager.GetClaimsAsync(user)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    UserPendings.Add(user.Email);
            }

            GetterAll getter = new GetterAll(_getterUtils, _context);

            List<Itinerario> ViajesUpdated = getter.GetAll("Itinerario").Where(i => (i as Itinerario).Update != 0).Select(i => i as Itinerario).ToList();
            //TODO: Change GetHashCode for updated.
            List<ViajeInvitado> InvitadosUpdated = getter.GetAll("ViajeInvitado").Where(vi => (vi as ViajeInvitado).GetHashCode() != 0).Select(vi => vi as ViajeInvitado).ToList();


            return (UserPendings, ViajesUpdated, InvitadosUpdated);
        }

    }
}
