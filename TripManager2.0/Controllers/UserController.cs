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
        public async Task<IActionResult> Welcome()
        {
            AdminService _adminService = new AdminService(_context, _userManager, _getterUtils);
            WorkflowServices _workflowServices = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            WelcomeViewModel vm = new WelcomeViewModel();
            var t = await _adminService.FillNotificationsAsync();
            vm.UserPendings = t.UserPendings;
            vm.ViajesUpdated = t.ViajesUpdated;


            if (vm.ViajesUpdated.Any() && User.HasClaim("Permission", "Common"))
            {
                if (User.Claims.Where(c => c.Type == "Visa").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteVisas).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                    }
                }

                if (User.Claims.Where(c => c.Type == "Passport").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendientePasaporte).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                    }
                }

                if (User.Claims.Where(c => c.Type == "JefeArea").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionJefeArea).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                    }
                }

                if (User.Claims.Where(c => c.Type == "Decano").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionDecano).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                    }
                }

                if (User.Claims.Where(c => c.Type == "Rector").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionRector).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                    }
                }

                var misViajes = vm.ViajesUpdated.Where(v => v.Usuario.Email == User.Identity.Name).ToList();
                if (misViajes.Count() != 0)
                {
                    for (int i = 0; i < misViajes.Count(); i++)
                    {
                        misViajes[i].Update = 0;
                        _workflowServices.UpdateItinerario(misViajes[i], misViajes[i]);
                    }
                }
            }
            return View(vm);
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

            services.RegisterItinerarioAsync(iterCmd);
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
            services.CreateItinerarioWorkflow(iterID, User.Claims.Where(x => x.Type == "Institucion").Single().Value);

            return RedirectToAction("Welcome");
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

    }
}


