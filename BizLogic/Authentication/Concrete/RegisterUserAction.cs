using BizData.Entities;
using BizDbAccess.Authentication;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Authentication.Concrete
{
    public class RegisterUserAction : BizActionErrors, IBizAction<RegisterUsuarioCommand, Usuario>
    {
        private readonly UserDbAccess _dbAccess;

        public RegisterUserAction(UserDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Usuario Action(RegisterUsuarioCommand dto)
        {
            var user = dto.ToUsuario();

            if (!HasErrors)
                _dbAccess.Add(user);

            return HasErrors ? null : user;

        }
    }
}
