using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterInstitucionAction : BizActionErrors, IBizAction<NameOnlyViewModel, Institucion>
    {
        private readonly InstitucionDbAccess _institucionDbAccess;

        public RegisterInstitucionAction(InstitucionDbAccess institucionDbAccess)
        {
            _institucionDbAccess = institucionDbAccess;
        }

        public Institucion Action(NameOnlyViewModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
