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
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
