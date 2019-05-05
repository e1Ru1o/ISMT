using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterCiudadAction : BizActionErrors, IBizAction<CiudadCommand, Ciudad>
    {
        private readonly CiudadDbAccess _ciudadDbAccess;

        public RegisterCiudadAction(CiudadDbAccess ciudadDbAccess)
        {
            _ciudadDbAccess = ciudadDbAccess;
        }

        public Ciudad Action(CiudadCommand dto)
        {
            throw new NotImplementedException();
        }
    }
}
