using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Shared
{
    public class NameOnlyViewModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
