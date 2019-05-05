using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class WorkflowViewModel : NameOnlyViewModel
    {
        [DisplayName("Estado del viaje de origen"), Required]
        public string EVOrigenName { get; set; }

        [DisplayName("Estado del viaje de destino"), Required]
        public string EVDestinoName { get; set; }

        [Required]
        public string ResponabilidadName { get; set; }
    }
}
