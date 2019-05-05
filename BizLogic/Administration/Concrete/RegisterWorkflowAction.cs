using BizData.Entities;
using BizDbAccess.Repositories;
using BizLogic.GenericInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Administration.Concrete
{
    public class RegisterWorkflowAction : BizActionErrors, IBizAction<WorkflowCommand, Workflow>
    {
        private readonly WorkflowDbAccess _workflowDbAccess;

        public RegisterWorkflowAction(WorkflowDbAccess workflowDbAccess)
        {
            _workflowDbAccess = workflowDbAccess;
        }

        public Workflow Action(WorkflowCommand dto)
        {
            throw new NotImplementedException();
        }
    }
}
