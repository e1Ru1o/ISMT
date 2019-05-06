using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Administration
{
    public class VisaViewModel : NameOnlyViewModel
    {
        [Required]
        public IEnumerable<string> paisesNames { get; set; }
    }
}
