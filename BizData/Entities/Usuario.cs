using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BizData.Entities
{
    public class Usuario : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }

        public virtual ICollection<Pasaporte> Pasaportes { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }
    }
}
