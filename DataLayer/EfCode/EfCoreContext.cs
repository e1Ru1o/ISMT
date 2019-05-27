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
        public DbSet<Itinerario> Itinerarios { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Region_Visa> Regiones_Visa { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Pais_Visa> Paises_Visas { get; set; }
        public DbSet<Visa> Visas { get; set; }
        public DbSet<ViajeInvitado> ViajesInvitados { get; set; }
        public DbSet<Historial> Historial { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pais_Visa>()
                .HasOne(pv => pv.Pais)
                .WithMany(p => p.Visas)
                .IsRequired();
                //.IsRequired(false)
                //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Pais_Visa>()
                .HasOne(pv => pv.Visa)
                .WithMany(v => v.Paises)
                .IsRequired();
            //.IsRequired(false)
            //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Historial>()
                    .HasOne(hist => hist.Usuario)
                    .WithMany(user => user.HistorialUpdater)
                    .HasForeignKey(hist => hist.UsuarioId);

            builder.Entity<Historial>()
                    .HasOne(hist => hist.UsuarioTarget)
                    .WithMany(user => user.HistorialTarget)
                    .HasForeignKey(hist => hist.UsuarioTargetId);

            //TODO: Config the new entities if is needed.
        }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

