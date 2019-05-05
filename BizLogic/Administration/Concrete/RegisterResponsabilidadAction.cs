using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterResponsabilidadAction : BizActionErrors, IBizAction<NameOnlyViewModel, Responsabilidad>
    {
        private readonly ResponsabilidadDbAccess _responsabilidadDbAccess;

        public RegisterResponsabilidadAction(ResponsabilidadDbAccess responsabilidadDbAccess)
        {
            _responsabilidadDbAccess = responsabilidadDbAccess;
        }

        public Responsabilidad Action(NameOnlyViewModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
