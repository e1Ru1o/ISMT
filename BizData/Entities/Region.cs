using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Region
    {
        public int RegionID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pais> Paises { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
