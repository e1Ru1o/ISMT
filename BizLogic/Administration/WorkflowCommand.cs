using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class WorkflowCommand : WorkflowViewModel
    {
        public EstadoViaje EstadoViajeOrigen { get; set; }
        public EstadoViaje EstadoViajeDestino { get; set; }
        public Responsabilidad Responsabilidad { get; set; }

        public WorkflowCommand(EstadoViaje estadoViajeOrigen, EstadoViaje estadoViajeDestino, Responsabilidad responsabilidad)
        {
            EstadoViajeOrigen = estadoViajeOrigen ?? throw new ArgumentNullException(nameof(estadoViajeOrigen));
            EstadoViajeDestino = estadoViajeDestino ?? throw new ArgumentNullException(nameof(estadoViajeDestino));
            Responsabilidad = responsabilidad ?? throw new ArgumentNullException(nameof(responsabilidad));
        }
    }
}
