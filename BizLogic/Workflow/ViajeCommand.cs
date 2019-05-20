using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow
{
    public class ViajeCommand : ViajeViewModel
    {
        public Usuario Usuario { get; set; }
        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }
        public Institucion Institucion { get; set; }
        public Itinerario Itinerario { get; set; }

        public Viaje ToViaje()
        {
            return new Viaje()
            {
                MotivoViaje = MotivoViaje,
                Usuario = Usuario,
                Pais = Pais,
                Ciudad = Ciudad,
                Institucion = Institucion
            };
        }
    }
}
