using BizData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Authentication
{
    public class RegisterUsuarioCommand : RegisterUsuarioViewModel
    {
        public Usuario ToUsuario()
        {
            return new Usuario
            {
                FirstName = FirstName,
                SecondName = SecondName,
                FirstLastName = FirstLastName,
                SecondLastName = SecondLastName,
                Email = Email,
                Password = Password
            };
        }
    }
}
