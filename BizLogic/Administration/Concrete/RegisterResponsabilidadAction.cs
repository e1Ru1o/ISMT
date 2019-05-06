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
        private readonly ResponsabilidadDbAccess _dbAccess;

        public RegisterResponsabilidadAction(ResponsabilidadDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Responsabilidad Action(NameOnlyViewModel dto)
        {
            var resp = new Responsabilidad()
            {
                Nombre = dto.Nombre
            };

            try
            {
                var result = _dbAccess.GetResponsabilidad(resp.Nombre);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe la responsabilidad {resp.Nombre}.");
            }

            if (!HasErrors)
                _dbAccess.Add(resp);

            return HasErrors ? null : resp;
        }
    }
}
