using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Historial
    {
        public int HistorialID { get; set; }
        public Estado Estado { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Itinerario Itinerario { get; set; }
    }
}
