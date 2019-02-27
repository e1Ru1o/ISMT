using System;
using System.Collections.Generic;
using System.Text;

namespace BizData.Entities
{
    public class Usuario
    {
        public long UsuarioID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Pasaporte> Pasaportes { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }
        public virtual ICollection<Usuario_Permiso> Permisos { get; set; }
    }
}
