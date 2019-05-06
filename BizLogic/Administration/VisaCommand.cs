using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class VisaCommand : VisaViewModel
    {
        public IEnumerable<Pais> Paises { get; set; }

        public VisaCommand(IEnumerable<Pais> paises)
        {
            Paises = paises;
        }
    }
}
