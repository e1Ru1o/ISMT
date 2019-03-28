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
        public virtual ICollection<Ciudad> Ciudades { get; set; }
        public virtual ICollection<Institucion> Instituciones { get; set; }
        public MotivoViaje MotivoViaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Costo { get; set; }

        public virtual EstadoViaje EstadoViaje { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Pais> Pais { get; set; }
    }
}
