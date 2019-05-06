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
        private readonly CiudadDbAccess _dbAccess;

        public RegisterCiudadAction(CiudadDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Ciudad Action(CiudadCommand dto)
        {
            var ciudad = dto.ToCiudad();

            try
            {
                var result = _dbAccess.GetCiudad(ciudad.Nombre, ciudad.Pais);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch(InvalidOperationException)
            {
                AddError($"Ya existe la ciudad {ciudad.Nombre} en {ciudad.Pais.Nombre}.");
            }

            if (!HasErrors)
                _dbAccess.Add(ciudad);

            return HasErrors ? null : ciudad;
        }
    }
}
