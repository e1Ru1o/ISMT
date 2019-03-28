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

            toUpd.EstadoViajeDestino = entity.EstadoViajeDestino;
            toUpd.EstadoViajeOrigen = entity.EstadoViajeOrigen;
            toUpd.Responsabilidad = entity.Responsabilidad;

            _context.Workflow.Update(toUpd);
            return toUpd;
        }

        public Workflow GetWorkflow(EstadoViaje origen, EstadoViaje destino)
        {
            return _context.Workflow.Where(w => w.EstadoViajeOrigen == origen &&
                                                w.EstadoViajeDestino == destino)
                                                .SingleOrDefault();
        }
    }
}
