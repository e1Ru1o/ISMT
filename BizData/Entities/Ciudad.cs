using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Ciudad
    {
        public int CiudadID { get; set; }
        public string Nombre { get; set; }
        public Pais Pais { get; set; }
    }
}
