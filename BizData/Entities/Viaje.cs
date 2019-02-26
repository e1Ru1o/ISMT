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
        public int UsuarioID { get; set; }
        public ICollection<Ciudad> Ciudades { get; set; }
        public ICollection<Institucion> Instituciones { get; set; }
        public MotivoViaje MotivoViaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Costo { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Pais> Pais { get; set; }
    }
}
