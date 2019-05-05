using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterPaisAction : BizActionErrors, IBizAction<NameOnlyViewModel, Pais>
    {
        private readonly PaisDbAccess _paisDbAccess;

        public RegisterPaisAction(PaisDbAccess paisDbAccess)
        {
            _paisDbAccess = paisDbAccess;
        }

        public Pais Action(NameOnlyViewModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
