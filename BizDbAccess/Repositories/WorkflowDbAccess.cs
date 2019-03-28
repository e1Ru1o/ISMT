using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.Repositories
{
    public class WorkflowDbAccess : IEntityDbAccess<Workflow>
    {
        public readonly EfCoreContext _context;

        public WorkflowDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public void Add(Workflow entity)
        {
            _context.Workflow.Add(entity);
            _context.Commit();
        }

        public void Delete(Workflow entity)
        {
            _context.Workflow.Remove(entity);
            _context.Commit();
        }

        public IEnumerable<Workflow> GetAll()
        {
            return _context.Workflow;
        }

        public void Update(Workflow entity)
        {
            throw new NotImplementedException();
        }
    }
}
