using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BizData.Entities;

namespace TripManager2._0.ViewModels
{
    public class InvitationViewModel
    {
        public InvitationViewModel() { }

        public InvitationViewModel(ViajeInvitado v)
        {
            Name = v.Nombre;
            Procedencia = v.Procedencia;
            End = v.FechaLLegada.Value;
            Status = v.Estado.ToString();
            ID = v.ViajeInvitadoID;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Procedencia { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public string Motivo { get; set; }

        public string Status { get; set; }

        public int ID { get; private set; }
    }
}
