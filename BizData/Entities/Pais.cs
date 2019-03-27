using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Pais 
    {
        public int PaisID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pais_Visa> Visas { get; set; }
    }
}
