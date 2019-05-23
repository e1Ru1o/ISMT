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

        public EditServices(IUnitOfWork context)
        {
            _context = context;
        }

    }
}
