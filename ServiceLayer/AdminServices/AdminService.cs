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
        private readonly RunnerWriteDb<NameOnlyViewModel, Responsabilidad> _runnerResponsabilidad;
        private readonly RunnerWriteDb<VisaCommand, Visa> _runnerVisa;
        private readonly RunnerWriteDb<WorkflowCommand, Workflow> _runnerWorkflow;

        private readonly PaisDbAccess _paisDbAccess;
        private readonly EstadoViajeDbAccess _estadoViajeDbAccess;
        private readonly ResponsabilidadDbAccess _responsabilidadDbAccess;

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
            _runnerResponsabilidad = new RunnerWriteDb<NameOnlyViewModel, Responsabilidad>(
                new RegisterResponsabilidadAction(new ResponsabilidadDbAccess(_context)), _context);
            _runnerVisa = new RunnerWriteDb<VisaCommand, Visa>(
                new RegisterVisaAction(new VisaDbAccess(_context)), _context);
            _runnerWorkflow = new RunnerWriteDb<WorkflowCommand, Workflow>(
                new RegisterWorkflowAction(new WorkflowDbAccess(_context)), _context);

            _paisDbAccess = new PaisDbAccess(_context);
            _estadoViajeDbAccess = new EstadoViajeDbAccess(_context);
            _responsabilidadDbAccess = new ResponsabilidadDbAccess(_context);
        }

        public long RegisterCiudad(CiudadCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Ciudad UpdateCiudad(Ciudad entity, Ciudad toUpd)
        {
            throw new NotImplementedException();
        }

        public void RemoveCiudad(Ciudad entity)
        {
            throw new NotImplementedException();
        }

        public long RegisterInstitucion(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Institucion UpdateInstitucion(Institucion entity, Institucion toUpd)
        {
            throw new NotImplementedException();
        }

        public void RemoveInstitucion(Institucion entity)
        {
            throw new NotImplementedException();
        }

        public long RegisterPais(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Pais UpdatePais(Pais entity, Pais toUpd)
        {
            throw new NotImplementedException();
        }

        public Pais GetPais(string nombre)
        {
            throw new NotImplementedException();
        }

        public void RemovePais(Pais entity)
        {
            throw new NotImplementedException();
        }

        public long RegisterResponsabilidad(NameOnlyViewModel vm, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Responsabilidad UpdateResponsabilidad(Responsabilidad entity, Responsabilidad toUpd)
        {
            throw new NotImplementedException();
        }

        public Responsabilidad GetResponsabilidad(string nombre)
        {
            throw new NotImplementedException();
        }

        public void RemoveResponsabilidad(Responsabilidad entity)
        {
            throw new NotImplementedException();
        }

        public long RegisterVisa(VisaCommand vm, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Visa UpdateVisa(Visa entity, Visa toUpd)
        {
            throw new NotImplementedException();
        }

        public void RemoveVisa(Visa entity)
        {
            throw new NotImplementedException();
        }

        public long RegisterWorkflow(WorkflowCommand cmd, out IImmutableList<ValidationResult> errors)
        {
            throw new NotImplementedException();
        }

        public Workflow UpdateWorkflow(Workflow entity, Workflow toUpd)
        {
            throw new NotImplementedException();
        } 

        public void RemoveWorkflow(Workflow entity)
        {
            throw new NotImplementedException();
        }
    }
}
