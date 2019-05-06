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
        private readonly InstitucionDbAccess _dbAccess;

        public RegisterInstitucionAction(InstitucionDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Institucion Action(NameOnlyViewModel dto)
        {
            var institucion = new Institucion()
            {
                Nombre = dto.Nombre
            };

            try
            {
                var result = _dbAccess.GetInstitucion(institucion.Nombre);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe la institucion {institucion.Nombre}.");
            }

            if (!HasErrors)
                _dbAccess.Add(institucion);

            return HasErrors ? null : institucion;
        }
    }
}
