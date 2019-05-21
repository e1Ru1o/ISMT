using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow
{
    public class ItinerarioCommand : ItinerarioViewModel
    {
        public Usuario Usuario { get; set; }

        public Itinerario ToItinerario()
        {
            return new Itinerario()
            {
                UsuarioID = Usuario.Id
            };
        }
    }
}
