using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Pais_Visa
    {
        public int Pais_VisaID { get; set; }
        public int PaisID { get; set; }
        public int ViajeID { get; set; }

        public Pais Pais { get; set; }
        public Viaje Viaje { get; set; }
    }
}
