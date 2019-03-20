using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.SharedServices
{
    public class EditServices
    {
        private readonly IUnitOfWork _context;
        private readonly PasaporteDbAccess _pasaporteDbAccess;

        public EditServices(IUnitOfWork context)
        {
            _context = context;
            _pasaporteDbAccess = new PasaporteDbAccess(_context);
        }

        public Pasaporte EditPasaporte(Pasaporte entity, Pasaporte toUpd)
        {
            return _pasaporteDbAccess.Update(entity, toUpd);
        }

    }
}
