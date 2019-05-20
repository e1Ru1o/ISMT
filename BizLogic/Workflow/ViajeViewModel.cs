using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Workflow
{
    public class ViajeViewModel
    {
        [Required]
        public string MotivoViaje { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        public string PaisName { get; set; }
        [Required]
        public string CiudadName { get; set; }
        [Required]
        public string InstitucionName { get; set; }

        public int ItinerarioID { get; set; }
    }
}
