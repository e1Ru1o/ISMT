using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using BizLogic.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterRegionAction : BizActionErrors, IBizAction<NameOnlyViewModel, Region>
    {
        private readonly RegionDbAccess _dbAccess;

        public RegisterRegionAction(RegionDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Region Action(NameOnlyViewModel dto)
        {
            var region = new Region()
            {
                Nombre = dto.Nombre
            };

            try
            {
                var result = _dbAccess.GetRegion(dto.Nombre);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe la ciudad {region.Nombre}.");
            }

            if (!HasErrors)
                _dbAccess.Add(region);

            return HasErrors ? null : region;
        }
    }
}
