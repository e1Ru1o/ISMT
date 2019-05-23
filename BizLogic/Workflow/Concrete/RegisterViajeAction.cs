using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow.Concrete
{
    public class RegisterViajeAction : BizActionErrors, IBizAction<ViajeCommand, Viaje>
    {
        private readonly ViajeDbAccess _dbAccess;

        public RegisterViajeAction(ViajeDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Viaje Action(ViajeCommand dto)
        {
            var viaje = dto.ToViaje();

            if (!HasErrors)
                _dbAccess.Add(viaje);

            if (dto.Itinerario.Viajes == null)
                dto.Itinerario.Viajes = new List<Viaje>();

            dto.Itinerario.Viajes.Add(viaje);

            return HasErrors ? null : viaje;
        }
    }
}
