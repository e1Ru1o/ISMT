using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Workflow
{
    public class ViajeCommand
    {
        public int ItinerarioID { get; set; }
        public string UsuarioId { get; set; }
        public string Motivo { get; set; }
        public string PaisName { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Pais Pais { get; set; }
        public Ciudad Ciudad { get; set; }
        //public Institucion Institucion { get; set; }
        public Itinerario Itinerario { get; set; }

        public ViajeCommand(int itinerarioID, string usuarioID, string paisName, string motivo, DateTime? fechaInicio, DateTime? fechaFin)
        {
            ItinerarioID = itinerarioID;
            UsuarioId = usuarioID;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            PaisName = paisName;
            Motivo = motivo;
        }

        public Viaje ToViaje()
        {
            return new Viaje()
            {
                Itinerario = Itinerario,
                MotivoViaje = Motivo,
                Pais = Pais,
                FechaInicio = FechaInicio,
                FechaFin = FechaFin,
                //Ciudad = Ciudad
               // Institucion = Institucion
            };
        }
    }
}
