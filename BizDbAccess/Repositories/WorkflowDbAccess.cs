using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        }

        public void Delete(Workflow entity)
        {
            _context.Workflow.Remove(entity);
        }

        public IEnumerable<Workflow> GetAll() => _context.Workflow;

        public Workflow Update(Workflow entity, Workflow toUpd)
        {
            if (toUpd == null)
                throw new InvalidOperationException("Workflow to be updated not exist");

            toUpd.EstadoViajeDestino = entity.EstadoViajeDestino ?? toUpd.EstadoViajeDestino;
            toUpd.EstadoViajeOrigen = entity.EstadoViajeOrigen ?? toUpd.EstadoViajeOrigen;
            toUpd.Responsabilidad = entity.Responsabilidad ?? toUpd.Responsabilidad;

            _context.Workflow.Update(toUpd);
            return toUpd;
        }

        /// <summary>
        /// Get a Workflow state given a pair of EstadoViaje.
        /// </summary>
        /// <param name="origen">The origin EstadoViaje.</param>
        /// <param name="destino">The destination EstadoViaje.</param>
        /// <returns>The Workflow if its the only object with that identifiers, otherwise throws a InvalidOperationException. Null if no exist such object</returns>
        public Workflow GetWorkflow(EstadoViaje origen, EstadoViaje destino)
        {
            return _context.Workflow.Where(w => w.EstadoViajeOrigen == origen &&
                                                w.EstadoViajeDestino == destino)
                                                .SingleOrDefault();
        }
    }
}
