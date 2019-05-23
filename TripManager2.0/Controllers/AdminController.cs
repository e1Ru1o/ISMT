using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using BizLogic.Administration;
using BizLogic.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AdminServices;
using System.Collections.Generic;
using System;
using System.Linq;
using TripManager2._0.ViewModels;
using ServiceLayer.AccountServices;
using BizLogic.Authentication;
using System.Threading.Tasks;
using BizLogic.Workflow;
using ServiceLayer.WorkFlowServices;
using System.Security.Claims;

namespace TripManager2._0.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController : Controller
    {
        private readonly GetterUtils _getterUtils;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUnitOfWork _context;
        private UserManager<Usuario> _userManager;

        public AdminController(IUnitOfWork context,
             SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IGetterUtils getterUtils)
        {
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult EditPais()
        {
            var getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Pais"));
        }
        [HttpPost]
        public IActionResult EditPais(int id)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            var getter = new GetterAll(_getterUtils, _context);
            var pais = ((getter.GetAll("Pais")) as IEnumerable<Pais>).Where(x => x.PaisID == id).Single();
            service.RemovePais(pais);
            return View(getter.GetAll("Pais"));
        }

        [HttpPost]
        public IActionResult AddPais(PaisCommand cmd)
        {

            if (ModelState.IsValid)
            {
                AdminService service = new AdminService(_context, _userManager, _getterUtils);
                service.RegisterPais(cmd, out var errors);
                return RedirectToAction("EditPais");
            }

            var getter = new GetterAll(_getterUtils, _context);
            cmd.Regiones = (getter.GetAll("Region") as IEnumerable<Region>).Select(x => x.Nombre);
            return View(cmd);
        }
        [HttpGet]
        public IActionResult AddPais()
        {
            var cmd = new PaisCommand();
            cmd.Regiones = (new GetterAll(_getterUtils, _context).GetAll("Region") as IEnumerable<Region>).Select(x => x.Nombre);
            return View(cmd);
        }
        [HttpGet]
        public IActionResult EditInstitucion()
        {
            var getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Institucion"));
        }
        [HttpPost]
        public IActionResult EditInstitucion(int id)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            var getter = new GetterAll(_getterUtils, _context);
            var ins = ((getter.GetAll("Institucion")) as IEnumerable<Institucion>).Where(x => x.InstitucionID == id).Single();
            service.RemoveInstitucion(ins);
            return View(getter.GetAll("Institucion"));
        }

        [HttpPost]
        public IActionResult AddInstitucion(NameOnlyViewModel cmd)
        {

            if (ModelState.IsValid)
            {
                AdminService service = new AdminService(_context, _userManager, _getterUtils);
                service.RegisterInstitucion(cmd, out var errors);
                return RedirectToAction("EditInstitucion");
            }

            return View(cmd);
        }
        [HttpGet]
        public IActionResult AddInstitucion()
        {
            return View(new NameOnlyViewModel());
        }
        [HttpGet]
        public IActionResult EditRegion()
        {
            var getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Region"));
        }
        [HttpPost]
        public IActionResult EditRegion(int id)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            var getter = new GetterAll(_getterUtils, _context);
            var ins = ((getter.GetAll("Region")) as IEnumerable<Region>).Where(x => x.RegionID == id).Single();
            service.RemoveRegion(ins);
            return View(getter.GetAll("Region"));
        }
        [HttpPost]
        public IActionResult AddRegion(NameOnlyViewModel cmd)
        {

            if (ModelState.IsValid)
            {
                AdminService service = new AdminService(_context, _userManager, _getterUtils);
                service.RegisterRegion(cmd, out var errors);
                return RedirectToAction("EditRegion");
            }

            return View(cmd);
        }
        [HttpGet]
        public IActionResult AddRegion()
        {
            return View(new NameOnlyViewModel());
        }

        public IActionResult EditUsuario()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            return View(getter.GetAll("Usuario"));
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUsuario(RegisterUsuarioCommand cmd)
        {

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            GetterAll getter1 = new GetterAll(_getterUtils, _context);
            var user = await _userManager.FindByEmailAsync(cmd.Email);
            if (ModelState.IsValid)
            {

                //var user = await _userManager.FindByEmailAsync(cmd.Email);
                LoginService loginService = new LoginService(_context, _signInManager, _userManager);
                var claim = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == "Permission").Select(x => x.Value).Single();
                await _userManager.RemoveClaimAsync(user, new Claim("Permission", claim));
                await _userManager.AddClaimAsync(user, new Claim("Permission", cmd.Level));

                try
                {
                    await _userManager.RemoveClaimAsync(user, new Claim("Passport", "True"));
                }
                finally
                {
                    if (cmd.Passaport == "True")
                        await _userManager.AddClaimAsync(user, new Claim("Passport", "True"));
                }
                try
                {
                    await _userManager.RemoveClaimAsync(user, new Claim("Visa", "True"));
                }
                finally
                {
                    if (cmd.Visa == "True")
                        await _userManager.AddClaimAsync(user, new Claim("Visa", "True"));
                }
                string ins = null;
                try
                {
                    ins = (await _userManager.GetClaimsAsync(user)).Where(x => x.Type == "Institucion").Select(x => x.Value).Single();

                    await _userManager.RemoveClaimAsync(user, new Claim("Institucion", ins));
                }catch{ }
                finally
                {
                    if (cmd.Institucion !="None") 
                        await _userManager.AddClaimAsync(user, new Claim("Institucion", cmd.Institucion));
                }
                var us = cmd.ToUsuario();
                await loginService.EditUserAsync(us, user);
                return RedirectToAction("EditUsuario", "Admin");
            }
            return View(cmd);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUsuario(string email)
        {


            var item = await _userManager.FindByEmailAsync(email);
            var claims = (await _userManager.GetClaimsAsync(item));
            var level = claims.Where(x => x.Type == "Permission").Select(x => x.Value).Single();
            string passport,visa,institucion;
            try
            {
                passport = claims.Where(x => x.Type == "Passport").Select(x => x.Value).Single();
            }
            catch
            {
                passport = "False";
            }
            try
            {
                visa = claims.Where(x => x.Type == "Visa").Select(x => x.Value).Single();
            }
            catch
            {
               visa = "False";
            }
            try
            {
                institucion = claims.Where(x => x.Type == "Institucion").Select(x => x.Value).Single();
            }
            catch
            {
                institucion = "None";
            }
            var cmd = new RegisterUsuarioCommand
            {
                EditEmail = item.UserName,
                Email = item.UserName,
                FirstName = item.FirstName,
                FirstLastName = item.FirstLastName,
                SecondLastName = item.SecondLastName,
                SecondName = item.SecondName,
                Password = "P9n$",
                Passaport = passport,
                Institucion = institucion,
                Visa=visa,
                Level = level,
            };
            return View(cmd);
        }
        [HttpGet]
        public async Task<IActionResult> PendingUsers()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            List<Usuario> pending = new List<Usuario>();
            foreach (var item in getter.GetAll("Usuario"))
            {
                if ((await _userManager.GetClaimsAsync(item as Usuario)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    pending.Add(item as Usuario);
            }

            PendingUsersViewModel vm = new PendingUsersViewModel()
            {
                Usuarios = pending,
            };

            return View(vm);
        }


        public async Task<IActionResult> PendingUsers(PendingUsersViewModel vm)
        {

            var user = await _userManager.FindByIdAsync(vm.userID);
            await _userManager.RemoveClaimAsync(user, new Claim("Pending", "true"));
            await _userManager.AddClaimAsync(user, new Claim("Pending", "false"));
            _context.Commit();

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            List<Usuario> pending = new List<Usuario>();
            foreach (var item in getter.GetAll("Usuario"))
            {
                if ((await _userManager.GetClaimsAsync(item as Usuario)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    pending.Add(item as Usuario);
            }
            return RedirectToAction("PendingUsers", "Admin");
        }

    }
}


