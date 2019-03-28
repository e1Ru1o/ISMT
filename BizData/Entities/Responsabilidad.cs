using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Responsabilidad
    {
        public int ResponsabilidadID { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Usuario_Responsabilidad> Usuarios { get; set; }
    }
}
