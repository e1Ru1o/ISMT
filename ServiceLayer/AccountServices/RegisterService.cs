using BizData.Entities;
using BizDbAccess.Authentication;
using BizDbAccess.GenericInterfaces;
using BizLogic.Authentication;
using BizLogic.Authentication.Concrete;
using DataLayer.EfCode;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.AccountServices
{
    public class RegisterService
    {
        private readonly RunnerWriteDb<RegisterUsuarioCommand, Usuario> _runner;

        public RegisterService(IUnitOfWork context)
        {
            _runner = new RunnerWriteDb<RegisterUsuarioCommand, Usuario>(
                new RegisterUserAction(new UserDbAccess(context)), context);
        }

        public long RegisterUsuario(RegisterUsuarioCommand cmd)
        {
            var user = _runner.RunAction(cmd);

            if (_runner.HasErrors) return 0;

            return user.UsuarioID;
        }
    }
}
