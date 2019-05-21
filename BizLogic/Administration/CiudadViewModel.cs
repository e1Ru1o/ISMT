using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class CiudadViewModel : NameOnlyViewModel
    {
        public string paisName { get; set; }

        public IEnumerable<string> Paises { get; set; }
    }
}
