using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Pasaporte_Visa
    {
        public int Pasaporte_VisaID { get; set; }
        public int PasaporteID { get; set; }
        public int VisaID { get; set; }

        public virtual Pasaporte Pasaporte { get; set; }
        public virtual Visa Visa { get; set; }
    }
}
