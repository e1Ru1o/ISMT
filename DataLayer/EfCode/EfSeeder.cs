using BizData.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfSeeder
    {
        private readonly EfCoreContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public EfSeeder(EfCoreContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Usuarios.Any())
            {

                var raul = new Usuario()
                {
                    FirstName = "Lazaro",
                    SecondName = "Raul",
                    FirstLastName = "Iglesias",
                    SecondLastName = "Vera",
                    Email = "raul@gmail.com",
                    Password = "1234",
                    Pasaportes = new List<Pasaporte>(),
                    Permission = PermisoTipo.admin
                };

                var pasaporte_raul = new Pasaporte()
                {
                    UsuarioCI = 97022206986,
                    FechaCreacion = new DateTime(2019, 1, 1),
                    FechaVencimiento = new DateTime(2019, 12, 31),
                    Actualizaciones = 0,
                    Tipo = PasaporteTipo.Americano,
                    Usuario = raul
                };

                raul.Pasaportes.Add(pasaporte_raul);

                _ctx.Add(raul);
                _ctx.Add(pasaporte_raul);

                _ctx.SaveChanges();

            }

            if (!_ctx.Usuarios.Where(x => x.FirstLastName == "Tenorio").Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "wwwroot/json/usuarios.json");
                var json = File.ReadAllText(filepath);
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);
                _ctx.Usuarios.AddRange(usuarios);

                _ctx.SaveChanges();
            }
        }
    }
}
