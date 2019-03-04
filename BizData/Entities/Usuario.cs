﻿using System.Collections.Generic;

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
        public PermisoTipo Permission { get; set; }

        public virtual ICollection<Pasaporte> Pasaportes { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }
    }
}
