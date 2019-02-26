using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public enum PermisoTipo
    {

    }

    public class Permiso
    {
        public int PermisoID { get; set; }
        public PermisoTipo Tipo { get; set; }

        public virtual ICollection<Usuario_Permiso> Usuarios { get; set; }
    }
}
