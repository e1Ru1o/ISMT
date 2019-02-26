using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Usuario_Permiso
    {
        public int Usuario_PermisoID { get; set; }
        public long UsuarioID { get; set; }
        public int PermisoID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Permiso Permiso { get; set; }
    }
}
