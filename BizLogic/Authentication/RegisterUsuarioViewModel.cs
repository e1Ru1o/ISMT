using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BizLogic.Authentication
{
    public class RegisterUsuarioViewModel
    {
        [Required, StringLength(100), DisplayName("Primer Nombre")]
        public string FirstName { get; set; }

        [StringLength(100), DisplayName("Segundo Nombre(Opcional)")]
        public string SecondName { get; set; }

        [Required, StringLength(100), DisplayName("Primer Apellido")]
        public string FirstLastName { get; set; }

        [Required, StringLength(100), DisplayName("Segundo Apellido")]
        public string SecondLastName { get; set; }

        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [Required, StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no son iguales")]
        public string ConfirmPassword { get; set; }

        public string EditEmail { get; set; }

        public string Level { get; set; }

        public string Passaport { get; set; }

        public string Visa { get; set; }

        public string Institucion { get; set; }
    }
}
