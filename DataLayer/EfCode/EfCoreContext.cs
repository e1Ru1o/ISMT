using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfCoreContext : DbContext, IUnitOfWork
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

