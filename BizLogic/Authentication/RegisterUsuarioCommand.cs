using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Authentication
{
    public class RegisterUsuarioCommand : RegisterUsuarioViewModel
    {
        public long UsuarioID { get; set; }
        public PermisoTipo Permission { get; set; }

        public Usuario ToUsuario()
        {
            return new Usuario
            {
                FirstName = FirstName,
                SecondName = SecondName,
                FirstLastName = FirstLastName,
                SecondLastName = SecondLastName,
                Email = Email,
                Password = Password,
                UsuarioID = UsuarioID,
                Permission = Permission
            };
        }

        public void SetViewModel(Usuario u)
        {
            FirstName = u.FirstName;
            SecondName = u.SecondName;
            FirstLastName = u.FirstLastName;
            SecondLastName = u.SecondLastName;
            Email = u.Email;
            Password = u.Password;
            ConfirmPassword = Password;
            UsuarioID = u.UsuarioID;
            Permission = u.Permission;
        }
    }
}
