using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Pais 
    {
        public string PaisID { get; set; }

        public virtual ICollection<Pais_Visa> Visas { get; set; }
    }
}
