using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using BizLogic.Workflow;
using ServiceLayer.BizRunners;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.WorkFlowServices
{
    public class WorkflowServices
    {
        private readonly IUnitOfWork _context;
        private readonly GetterUtils _getterUtils;

        private readonly RunnerWriteDb<ItinerarioCommand, Itinerario> _runnerItinerario;
        private readonly RunnerWriteDb<ViajeCommand, Viaje> _runnerViaje;

        public WorkflowServices(IUnitOfWork context, GetterUtils getterUtils)
        {
            _context = context;
            _getterUtils = getterUtils;

            //_runnerItinerario = new RunnerWriteDb<ItinerarioCommand, Itinerario>(
            //    new RegisterI)
        }

       // public 
    }
}
