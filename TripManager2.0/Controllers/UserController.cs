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
using System.Linq;
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

        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
        	var getter = new GetterAll(_getterUtils, _context);
        	var data = getter.GetAll("Pais").Select(x => (x as Pais).Nombre);
        	var trip = new ViajeViewModel();
        	trip.Posibilities = data.ToList();
            
            return View(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViajeViewModel vm)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils);

            var iterCmd = new ItinerarioCommand()
            {
                UsuarioID = _userManager.GetUserId(User)
            };

            var iterID =  await services.RegisterItinerarioAsync(iterCmd);

            for (int i = 0; i < vm.Country.Count(); i++)
            {
                var viajeCmd = new ViajeCommand(iterID, iterCmd.UsuarioID, vm.Country[i], vm.Motivo[i], vm.Start[i], vm.End[i]);
                await services.RegisterViajeAsync(viajeCmd);
            }

            return View("Welcome");
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

        [HttpPost]
        public async Task<IActionResult> PendingUsers(PendingUsersViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.userID);
            await _userManager.RemoveClaimAsync(user, new Claim("Pending", "true"));
            await _userManager.AddClaimAsync(user, new Claim("Pending", "false"));
            await _userManager.AddClaimAsync(user, new Claim("Permission", "inversionista"));
            _context.Commit();

            GetterAll getter = new GetterAll(_getterUtils, _context, _signInManager, _userManager);
            List<Usuario> pending = new List<Usuario>();
            foreach (var item in getter.GetAll("Usuario"))
            {
                if ((await _userManager.GetClaimsAsync(item as Usuario)).Any(c => c.Type == "Pending" && c.Value == "true"))
                    pending.Add(item as Usuario);
            }
            return RedirectToAction("PendingUsers", "User");
        }

    }
}


