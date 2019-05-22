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

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
        	var getter = new GetterAll(_getterUtils, _context);
        	var data = getter.GetAll("Pais").Select(x => (x as Pais).Nombre);
            
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ViajeViewModel vm)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);

            var iterCmd = new ItinerarioCommand()
            {
                UsuarioID = _userManager.GetUserId(User)
            };

            services.RegisterItinerarioAsync(iterCmd, User.Claims.Where(x=> x.Type == "Institucion").Single().Value);
            var user = await _userManager.GetUserAsync(User);
            var iterID = user.Itinerarios.Last().ItinerarioID;

            for (int i = 0; i < vm.Motivo.Count; ++i)
                if (vm.Motivo[i] is null)
                    vm.Motivo[i] = "";

            for (int i = 0; i < vm.Country.Count(); i++)
            {
                var viajeCmd = new ViajeCommand(iterID, user.Id, vm.Country[i], vm.City[i], vm.Motivo[i], vm.Start[i], vm.End[i]);
                services.RegisterViajeAsync(viajeCmd);
            }

            services.CalculateDates(services.GetItinerario(iterID));

            return View("Welcome");
        }

        [HttpGet]
        public async Task<IActionResult> ViewTrips()
        {
            //TODO: [KARL LEWIS] When you create TripDetailsView add funtionality to the DetailsButton in the View correspondent to this method
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var data = services.GetItinerarioNotFinished(user)
                .Select(x => new TripViewModel(x.FechaInicio.Value, x.FechaFin.Value, x.Estado.ToString(), x.ItinerarioID));
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ViewTrips(int canceled)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            services.CancelItinerario(canceled, user.Id, "El usuario cancelo su viaje");
            return RedirectToAction("ViewTrips");
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
            cmd.Regiones=(getter.GetAll("Region") as IEnumerable<Region>).Select(x=>x.Nombre);
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


