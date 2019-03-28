using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Usuario_Responsabilidad
    {
        public int ID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Responsabilidad Responsabilidad { get; set; }
    }
}
