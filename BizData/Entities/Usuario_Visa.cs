using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Usuario_Visa
    {
        public int Usuario_VisaID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Visa Visa { get; set; }
    }
}
