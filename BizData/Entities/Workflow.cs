using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Workflow
    {
        public int WorkflowID { get; set; }

        public virtual EstadoViaje EstadoViajeOrigen { get; set; }
        public virtual EstadoViaje EstadoViajeDestino { get; set; }
        public virtual Responsabilidad Responsabilidad { get; set; }
    }
}
