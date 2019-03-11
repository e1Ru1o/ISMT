using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Pais_Visa
    {
        public int Pais_VisaID { get; set; }

        public virtual Pais Pais { get; set; }
        public virtual Viaje Viaje { get; set; }
    }
}
