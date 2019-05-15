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
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Pais_Visa> Paises_Visas { get; set; }
        public DbSet<Visa> Visas { get; set; }
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

            builder.Entity<Pasaporte>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pasaportes)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Usuario_Responsabilidad>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.Responsabilidades)
                .IsRequired();
                //.IsRequired(false)
                //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Usuario_Responsabilidad>()
                .HasOne(ur => ur.Responsabilidad)
                .WithMany(r => r.Usuarios)
                .IsRequired();
                //.IsRequired(false)
                //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Viaje>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Viajes)
                .IsRequired();           
        }

        public int Commit()
        {
            return SaveChanges();
        }
    }
}

