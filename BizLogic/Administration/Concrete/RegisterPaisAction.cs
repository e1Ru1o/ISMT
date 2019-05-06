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
        private readonly PaisDbAccess _dbAccess;

        public RegisterPaisAction(PaisDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Pais Action(NameOnlyViewModel dto)
        {
            var pais = new Pais()
            {
                Nombre = dto.Nombre
            };

            try
            {
                var result = _dbAccess.GetPais(pais.Nombre);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe el pais {pais.Nombre}.");
            }

            if (!HasErrors)
                _dbAccess.Add(pais);

            return HasErrors ? null : pais;
        }
    }
}
