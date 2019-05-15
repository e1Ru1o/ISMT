using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Historial
    {
        public int HistorialID { get; set; }
        public string EstadoViaje { get; set; }

        public virtual Itinerario Viaje { get; set; }
        //The owner of Viaje can be get through Viaje
        //as well of the dates.
    }
}
