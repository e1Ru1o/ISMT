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
        [Required, StringLength(20)]
        public string RegionName { get; set; }
    }
}
