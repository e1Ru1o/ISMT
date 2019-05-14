using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Visa
    {
        public int VisaID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pais_Visa> Paises { get; set; }
    }
}
