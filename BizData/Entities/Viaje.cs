using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public enum MotivoViaje
    {

    }

    public class Viaje
    {
        public int ViajeID { get; set; }
        public string MotivoViaje { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        
        public virtual Itinerario Itinerario { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        public virtual Institucion Institucion { get; set; }
    }
}
