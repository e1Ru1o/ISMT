using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration
{
    public class PaisCommand : PaisViewModel
    {
        public Region Region { get; set; }

        public PaisCommand(Region region)
        {
            Region = region;
        }

        public Pais ToPais()
        {
            return new Pais
            {
                Nombre = Name,
                Region = Region
            };
        }
    }
}
