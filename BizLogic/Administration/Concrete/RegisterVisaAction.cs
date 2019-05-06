using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterVisaAction : BizActionErrors, IBizAction<VisaCommand, Visa>
    {
        private readonly VisaDbAccess _dbAccess;

        public RegisterVisaAction(VisaDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Visa Action(VisaCommand dto)
        {
            var visa = dto.ToVisa();

            try
            {
                var result = _dbAccess.GetVisa(visa.Name);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe la visa {visa.Name}.");
            }

            if (!HasErrors)
                _dbAccess.Add(visa);

            return HasErrors ? null : visa;
        }
    }
}
