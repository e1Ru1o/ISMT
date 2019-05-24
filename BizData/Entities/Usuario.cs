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
        public bool HasPassport { get; set; }

        public virtual ICollection<Itinerario> Itinerarios { get; set; }
        public virtual ICollection<ViajeInvitado> ViajesInvitado { get; set; }
        public virtual ICollection<Usuario_Visa> Visas { get; set; }
        public virtual ICollection<Historial> HistorialTarget { get; set; }
        public virtual ICollection<Historial> HistorialUpdater { get; set; }
    }
}
