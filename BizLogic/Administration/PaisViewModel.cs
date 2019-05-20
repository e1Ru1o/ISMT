using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class PaisViewModel
    {
        [Required, StringLength(15)]
        public string Name { get; set; }
        [StringLength(20)]
        public string RegionName { get; set; }
        public IEnumerable<string> Regiones { get; set; }
    }
}
