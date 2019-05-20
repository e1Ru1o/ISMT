using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.WorkFlow.Concrete
{
    public class RegisterHistorialAction : BizActionErrors, IBizAction<Historial, Historial>
    {
        private readonly HistorialDbAccess _dbAccess;

        public RegisterHistorialAction(HistorialDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Historial Action(Historial dto)
        {
            try
            {
                var result = _dbAccess.GetHistorial(dto.EstadoViaje, dto.Viaje);

                if (result != null)
                    throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                AddError($"Ya existe ese historial.");
            }

            if (!HasErrors)
                _dbAccess.Add(dto);

            return HasErrors ? null : dto;
        }
    }
}
