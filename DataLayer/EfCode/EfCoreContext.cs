using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfCoreContext : IdentityDbContext<Usuario>, IUnitOfWork
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Pasaporte> Pasaportes { get; set; }
        public DbSet<Responsabilidad> Responsabilidades { get; set; }
        public DbSet<EstadoViaje> EstadosViaje { get; set; }
        public DbSet<Workflow> Workflow { get; set; }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

