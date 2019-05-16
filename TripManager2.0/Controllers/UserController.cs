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
using TripManager2._0.ViewModels;
using System.Linq;
using ServiceLayer.AccountServices;
using BizLogic.Authentication;
using System.Threading.Tasks;

namespace TripManager2._0.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UserController : Controller
    {
        private readonly GetterUtils _getterUtils;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUnitOfWork _context;
        private UserManager<Usuario> _userManager;

        public UserController(IUnitOfWork context,
             SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IGetterUtils getterUtils)
        {
            _context = context;
            _getterUtils = (GetterUtils)getterUtils;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Welcome(TableUserViewModel user)
        {
            user.per = PermisoTipo.admin;
            return View(user);
        }

        public IActionResult Print(TableUserViewModel text)
        {
            GetterAll getter;
            if(text.Table.ToString() == "Usuario")
                getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            else
                getter = new GetterAll(_getterUtils, _context);
            var result = getter.GetAll(text.Table.ToString());

            return View("Display" + text.Table.ToString(), result);
        }
        [HttpGet]
        public IActionResult EditCiudad()
        {
            var getter = new GetterAll(_getterUtils, _context);
            return View(getter.GetAll("Ciudad"));
        }
         [HttpPost]
        public IActionResult EditCiudad(int id)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            var getter = new GetterAll(_getterUtils, _context);
            var ciudad = ((getter.GetAll("Ciudad")) as IEnumerable<Ciudad>).Where(x=>x.CiudadID==id).Single();
            service.RemoveCiudad(ciudad);
            return View(getter.GetAll("Ciudad"));
        }
        public IActionResult AddCiudad(CiudadCommand cmd)
        {

            if(ModelState.IsValid)
            {
                AdminService service = new AdminService(_context, _userManager, _getterUtils);
                service.RegisterCiudad(cmd, out var errors);
                return RedirectToAction("EditCiudad");
            }
            cmd.Paises = (new GetterAll(_getterUtils, _context).GetAll("Pais") as IEnumerable<Pais>).Select(x => x.Nombre);
            return View(cmd);
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
        public IActionResult AddPais(PaisCommand cmd)
        {

            if (ModelState.IsValid)
            {
                AdminService service = new AdminService(_context, _userManager, _getterUtils);
                service.RegisterPais(cmd, out var errors);
                return RedirectToAction("EditPais");
            }

            var getter = new GetterAll(_getterUtils, _context);
            cmd.Regiones=(getter.GetAll("Region") as IEnumerable<Region>).Select(x=>x.Nombre);
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

        public IActionResult EditUsuario()
        {
            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            return View(getter.GetAll("Usuario"));
        }

        public async Task<IActionResult> UpdateUsuario(RegisterUsuarioCommand cmd)
        {

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            GetterAll getter1 = new GetterAll(_getterUtils, _context);
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(cmd.Email);
                LoginService loginService = new LoginService(_context, _signInManager, _userManager);
                var us = cmd.ToUsuario();
                await loginService.EditUserAsync(us, (getter.GetAll("Usuario") as IEnumerable<Usuario>).Where(x => x.Email == cmd.EditEmail).Single());
                return RedirectToAction("EditUsuario", "User");
            }
            return View(cmd);
        }

    }
}


