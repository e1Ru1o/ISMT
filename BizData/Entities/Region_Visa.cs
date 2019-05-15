using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Region_Visa
    {
        public int Region_VisaID { get; set; }

        public virtual Region Region { get; set; }
        public virtual Visa Visa { get; set; }
    }
}
