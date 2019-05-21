using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow.Concrete
{
    public class RegisterItinerarioAction : BizActionErrors, IBizAction<ItinerarioCommand, Itinerario>
    {
        private readonly ItinerarioDbAccess _dbAccess;

        public RegisterItinerarioAction(ItinerarioDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Itinerario Action(ItinerarioCommand dto)
        {
            var itinerario = dto.ToItinerario();
            if (dto.Usuario.Itinerarios == null)
                dto.Usuario.Itinerarios = new List<Itinerario>();

            if (!HasErrors)
                _dbAccess.Add(itinerario);

            dto.Usuario.Itinerarios.Add(itinerario);

            return HasErrors ? null : itinerario;
        }
    }
}
