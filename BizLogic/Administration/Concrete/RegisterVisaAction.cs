using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterVisaAction : BizActionErrors, IBizAction<NameOnlyViewModel, Visa>
    {
        private readonly VisaDbAccess _visaDbAccess;

        public RegisterVisaAction(VisaDbAccess visaDbAccess)
        {
            _visaDbAccess = visaDbAccess;
        }

        public Visa Action(NameOnlyViewModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
