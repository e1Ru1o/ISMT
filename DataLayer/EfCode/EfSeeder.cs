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
using System.Threading.Tasks;

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

        public async Task Seed()
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
                    UserName = "raul@gmail.com"
                };

                await _userManager.CreateAsync(raul, "T3n!");
                await _userManager.AddClaimAsync(raul, new Claim("Permission", "Admin"));
                await _userManager.AddClaimAsync(raul, new Claim("Pending", "false"));
                await _userManager.AddClaimAsync(raul, new Claim("Cargo", "comun"));

                var region = new Region
                {
                    Nombre = "Ninguna"
                };

                var cuba = new Pais()
                {
                    Nombre = "Cuba",
                    Region = region
                };

                /*var filepath = Path.Combine(_hosting.ContentRootPath, "wwwroot/json/usuarios.json");
                var json = File.ReadAllText(filepath);
                var usuarios = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(json);

                foreach (var user in usuarios)
                {
                    _userManager.CreateAsync(user, "1234");
                   // _userManager.AddClaimAsync(user, new Claim("Permission", "common"));
                }*/

                _ctx.SaveChanges();
            }
   
        }
    }
}
