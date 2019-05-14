﻿using BizData.Entities;
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

namespace ServiceLayer.AdminServices
{
    public class AdminService
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly GetterUtils _getterUtils;

        private readonly RunnerWriteDb<CiudadCommand, Ciudad> _runnerCiudad;
        private readonly RunnerWriteDb<NameOnlyViewModel, Institucion> _runnerInstitucion;
        private readonly RunnerWriteDb<NameOnlyViewModel, Pais> _runnerPais;
        private readonly RunnerWriteDb<VisaCommand, Visa> _runnerVisa;
        private readonly RunnerWriteDb<NameOnlyViewModel, Region> _runnerWriteDb;

        private readonly PaisDbAccess _paisDbAccess;
        private readonly CiudadDbAccess _ciudadDbAccess;
        private readonly InstitucionDbAccess _institucionDbAccess;
        private readonly Pais_VisaDbAccess _pais_VisaDbAccess;
        private readonly VisaDbAccess _visaDbAccess;
        private readonly RegionDbAccess _regionDbAccess;

        public AdminService(IUnitOfWork context, UserManager<Usuario> userManager, GetterUtils getterUtils)
        {
            _context = context;
            _userManager = userManager;
            _getterUtils = getterUtils;

            _runnerCiudad = new RunnerWriteDb<CiudadCommand, Ciudad>(
                new RegisterCiudadAction(new CiudadDbAccess(_context)), _context);
            _runnerInstitucion = new RunnerWriteDb<NameOnlyViewModel, Institucion>(
                new RegisterInstitucionAction(new InstitucionDbAccess(_context)), _context);
            _runnerPais = new RunnerWriteDb<NameOnlyViewModel, Pais>(
                new RegisterPaisAction(new PaisDbAccess(_context)), _context);
            _runnerVisa = new RunnerWriteDb<VisaCommand, Visa>(
                new RegisterVisaAction(new VisaDbAccess(_context)), _context);

            _paisDbAccess = new PaisDbAccess(_context);
            _ciudadDbAccess = new CiudadDbAccess(_context);
            _institucionDbAccess = new InstitucionDbAccess(_context);
            _pais_VisaDbAccess = new Pais_VisaDbAccess(_context);
            _visaDbAccess = new VisaDbAccess(_context);
        }

        public long RegisterCiudad(CiudadCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            cmd.Pais = _paisDbAccess.GetPais(cmd.paisName);

            var ciudad = _runnerCiudad.RunAction(cmd);

            if (_runnerCiudad.HasErrors)
            {
                errors = _runnerCiudad.Errors;
                return -1;
            }

            errors = null;
            return ciudad.CiudadID;
        }

        public Ciudad UpdateCiudad(Ciudad entity, Ciudad toUpd)
        {
            var ciudad = _ciudadDbAccess.Update(entity, toUpd);
            _context.Commit();
            return ciudad;
        }

        public void RemoveCiudad(Ciudad entity)
        {
            _ciudadDbAccess.Delete(entity);
            _context.Commit();
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

        public long RegisterPais(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            var pais = _runnerPais.RunAction(vm);

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

        public long RegisterVisa(VisaCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            cmd.Paises = _paisDbAccess.GetAll().Zip(cmd.paisesNames, (Pais p, string s) =>
            {
                return p.Nombre == s ? p : null;
            })
            .Where(p => p != null)
            .ToList();

            cmd.PaisesVisas = BuildListOfPais_Visa(cmd.Paises, new List<Visa>() { new Visa() { Name = cmd.Nombre } });

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
            var region = _runnerPais.RunAction(vm);

            if (_runnerPais.HasErrors)
            {
                errors = _runnerPais.Errors;
                return -1;
            }

            errors = null;
            return region.PaisID;
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
    }
}
