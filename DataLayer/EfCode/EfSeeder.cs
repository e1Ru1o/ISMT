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
                await _userManager.AddClaimAsync(raul, new Claim("Institucion", "Rector"));
                await _userManager.AddClaimAsync(raul, new Claim("Passport", "true"));
                await _userManager.AddClaimAsync(raul, new Claim("Visa", "true"));

                var pancho = new Usuario()
                {
                    FirstName = "pancho",
                    SecondName = "man",
                    FirstLastName = "matias",
                    SecondLastName = "Vera",
                    Email = "pa@gmail.com",
                    UserName = "pa@gmail.com"
                };

                await _userManager.CreateAsync(pancho, "T3n!");
                await _userManager.AddClaimAsync(pancho, new Claim("Permission", "Admin"));
                await _userManager.AddClaimAsync(pancho, new Claim("Pending", "false"));
                await _userManager.AddClaimAsync(pancho, new Claim("Institucion", "Rector"));
                await _userManager.AddClaimAsync(pancho, new Claim("Passport", "true"));
                await _userManager.AddClaimAsync(pancho, new Claim("Visa", "true"));

                var marta = new Usuario()
                {
                    FirstName = "marta",
                    SecondName = "rita",
                    FirstLastName = "pol",
                    SecondLastName = "Gise",
                    Email = "ma@gmail.com",
                    UserName = "ma@gmail.com"
                };

                await _userManager.CreateAsync(marta, "T3n!");
                await _userManager.AddClaimAsync(marta, new Claim("Permission", "Admin"));
                await _userManager.AddClaimAsync(marta, new Claim("Pending", "false"));
                await _userManager.AddClaimAsync(marta, new Claim("Institucion", "Rector"));
                await _userManager.AddClaimAsync(marta, new Claim("Passport", "true"));
                await _userManager.AddClaimAsync(marta, new Claim("Visa", "true"));

                // Regiones
                var region = new Region { Nombre = "Ninguna"};
                _ctx.Regiones.Add(region);
                _ctx.SaveChanges();

                region = new Region { Nombre = "America" };
                _ctx.Regiones.Add(region);
                _ctx.SaveChanges();

                region = new Region { Nombre = "Europa" };
                _ctx.Regiones.Add(region);
                _ctx.SaveChanges();

                region = new Region { Nombre = "Pangea" };
                _ctx.Regiones.Add(region);
                _ctx.SaveChanges();

                //Paises
                var pais = new Pais()
                {
                    Nombre = "Cuba",
                    Region = _ctx.Regiones.Find(2)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Panama",
                    Region = _ctx.Regiones.Find(2)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Uruguay",
                    Region = _ctx.Regiones.Find(2)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Italia",
                    Region = _ctx.Regiones.Find(3)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Alemania",
                    Region = _ctx.Regiones.Find(3)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Inglaterra",
                    Region = _ctx.Regiones.Find(3)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Sudafrica",
                    Region = _ctx.Regiones.Find(4)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Australia",
                    Region = _ctx.Regiones.Find(4)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                pais = new Pais()
                {
                    Nombre = "Japon",
                    Region = _ctx.Regiones.Find(4)
                };
                _ctx.Paises.Add(pais);
                _ctx.SaveChanges();

                //Visas
                var visa = new Visa() { Name = "URUGUAY_VISA" };
                _ctx.Visas.Add(visa);
                _ctx.SaveChanges();

                visa = new Visa() { Name = "EXCHANGE" };
                _ctx.Visas.Add(visa);
                _ctx.SaveChanges();

                visa = new Visa() { Name = "PANGEA_VISA" };
                _ctx.Visas.Add(visa);
                _ctx.SaveChanges();

                //Visa_Pais
                var visa_pais = new Pais_Visa()
                {
                    Pais = _ctx.Paises.Find(3),
                    Visa = _ctx.Visas.Find(1)
                };
                _ctx.Paises_Visas.Add(visa_pais);
                _ctx.SaveChanges();

                //Visa_Region
                var visa_region = new Region_Visa()
                {
                    Visa = _ctx.Visas.Find(2),
                    Region = _ctx.Regiones.Find(3)
                };
                _ctx.Regiones_Visa.Add(visa_region);
                _ctx.SaveChanges();

                visa_region = new Region_Visa()
                {
                    Visa = _ctx.Visas.Find(3),
                    Region = _ctx.Regiones.Find(4)
                };
                _ctx.Regiones_Visa.Add(visa_region);
                _ctx.SaveChanges();

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
