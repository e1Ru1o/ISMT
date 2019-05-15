using BizData.Entities;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow.Concrete
{
    public class RegisterItinerarioAction : BizActionErrors, IBizAction<ItinerarioCommand, Itinerario>
    {
        public Itinerario Action(ItinerarioCommand dto)
        {
            throw new NotImplementedException();
        }
    }
}
