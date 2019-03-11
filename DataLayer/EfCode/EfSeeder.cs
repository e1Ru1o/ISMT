using BizData.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DataLayer.EfCode
{
    public class EfSeeder
    {
        private readonly EfCoreContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<Usuario> _userManager;

        public EfSeeder(EfCoreContext ctx, IHostingEnvironment hosting,
            UserManager<Usuario> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (_userManager.FindByEmailAsync("raul@gmail.com").Result == null)
            {
                var raul = new Usuario()
                {
                    FirstName = "Lazaro",
                    SecondName = "Raul",
                    FirstLastName = "Iglesias",
                    SecondLastName = "Vera",
                    Email = "raul@gmail.com",
                    Pasaportes = new List<Pasaporte>(),
                    UserName = "raul@gmail.com"
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

                _userManager.CreateAsync(raul, "T3n!");
                _ctx.Add(pasaporte_raul);

                /*var filepath = Path.Combine(_hosting.ContentRootPath, "wwwroot/json/usuarios.json");
                var json = File.ReadAllText(filepath);
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);

                foreach (var user in usuarios)
                {
                    _userManager.CreateAsync(user, "1234");
                   // _userManager.AddClaimAsync(user, new Claim("Permission", "common"));
                }*/

                _ctx.SaveChanges();
                _userManager.AddClaimAsync(raul, new Claim("Permission", "admin"));
            }
   
        }
    }
}
