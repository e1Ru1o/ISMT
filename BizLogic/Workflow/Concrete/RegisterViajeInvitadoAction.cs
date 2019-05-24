using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow.Concrete
{
    public class RegisterViajeInvitadoAction : BizActionErrors, IBizAction<ViajeInvitado, ViajeInvitado>
    {
        private readonly ViajeInvitadoDbAccess _dbAccess;

        public RegisterViajeInvitadoAction(ViajeInvitadoDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public ViajeInvitado Action(ViajeInvitado dto)
        {
            try
            {
                var result = _dbAccess.GetViajeInvitado(dto.Nombre, dto.Procedencia);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe el viaje invitado con viajero {dto.Nombre} de {dto.Procedencia}.");
            }

            if (!HasErrors)
                _dbAccess.Add(dto);

            return HasErrors ? null : dto;
        }
    }
}
