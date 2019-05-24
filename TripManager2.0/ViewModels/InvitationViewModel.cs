using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.ViewModels
{
    public class InvitationViewModel
    {
        [Required]
        [DisplayName("Nombre y apellidos del invitado")]
        public string Name { get; set; }

        [Required]
        public string Procedencia { get; set; }

        [DisplayName("Fecha de llegada")]
        [Required]
        public DateTime End { get; set; }

        public string Motivo { get; set; }
    }
}
