using Microsoft.AspNetCore.Mvc;
using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using BizLogic.Administration;
using BizLogic.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripManager2._0.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class EditorController : Controller
    {

        private readonly GetterUtils _getterUtils;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IUnitOfWork _context;
        private UserManager<Usuario> _userManager;

        public EditorController(IUnitOfWork context,
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
        [Authorize("Boss")]
        public async Task<IActionResult> AuthorizeTrip()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var vm = new PendingUserTripViewModel();
            vm.Users = services.GetItinerariosEstado(
                Enum.Parse<Estado>($"PendienteAprobacion{User.Claims.Where(x => x.Type == "Institucion").Single().Value}"),
                user
            ).Select(x => new UserTripViewModel(x.FechaInicio.Value, x.FechaFin.Value, x.Estado.ToString(), x.ItinerarioID, x.Usuario));
            vm.Visitants = new List<InvitationViewModel>();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizeTrip(int tripId, int action, int uType, string motivo)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (uType == 0)
            {
                if (action == 0)
                    services.ManageActionAprobar(tripId, user.Id, motivo);
                else if (action == 1)
                    services.ManageActionRechazar(tripId, user.Id, motivo);
                else
                    services.CancelItinerario(tripId, user.Id, motivo);
            }
            else
            {
                if (action == 0)
                    services.ManageActionAprobarViajeInvitado(tripId, user.Id, motivo);
                else if (action == 1)
                    services.ManageActionRechazarViajeInvitado(tripId, user.Id, motivo);
                else
                    services.CancelViajeInvitado(tripId, user.Id, motivo);
            }

            return Redirect("AuthorizeTrip");
        }

        [HttpGet]
        [Authorize("Passport")]
        public async Task<IActionResult> AuthorizePassport()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var data = services.GetUsuariosPendientePasaporte(user);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizePassport(string usuarioId, int action, int uType, string motivo)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (action == 0)
            {
                services.SetPassportToUser(usuarioId);
                services.ManageActionPasaporte(usuarioId, user.Id, BizLogic.WorkflowManager.Action.Aprobar, motivo);
            }
            else if (action == 1)
                services.ManageActionPasaporte(usuarioId, user.Id, BizLogic.WorkflowManager.Action.Rechazar, motivo);
            else
                services.ManageActionPasaporte(usuarioId, user.Id, BizLogic.WorkflowManager.Action.Cancelar, motivo);

            return Redirect("AuthorizePassport");
        }

        [HttpGet]
        [Authorize("Visa")]
        public async Task<IActionResult> GiveVisa()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var data = services.GetVisasUsuarioVisasPendiente(user);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> GiveVisa(string uID, int vID, int action, string motivo)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (action == 0)
            {
                services.SetVisaToUser(uID, vID, user.Id);
                services.ManageActionVisa(uID, user.Id, vID, BizLogic.WorkflowManager.Action.Aprobar, motivo);
            }
            else if (action == 1)
                services.ManageActionVisa(uID, user.Id, vID, BizLogic.WorkflowManager.Action.Rechazar, motivo);


            return RedirectToAction("GiveVisa");
        }

        [HttpGet]
        [Authorize("Visa")]
        public IActionResult CreateVisa()
        {
            var getter = new GetterAll(_getterUtils, _context);
            var countries = getter.GetAll("Pais").Select(x => (x as Pais).Nombre);
            var regions = getter.GetAll("Region").Select(x => (x as Region).Nombre);
            return View(new VisaViewModel() { paisesNames = countries, regionesName = regions });
        }

        [HttpPost]
        public IActionResult CreateVisa(VisaViewModel vm)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            VisaCommand cmd = new VisaCommand()
            {
                Nombre = vm.Nombre,
                paisesNames = vm.paisesNames,
                regionesName = vm.regionesName
            };

            service.RegisterVisa(cmd, out var errors);

            return RedirectToAction("Welcome", "User");
        }

        [Authorize("Visa")]
        public IActionResult EditVisa()
        {
            var getter = new GetterAll(_getterUtils, _context);
            var visas = (getter.GetAll("Visa") as IEnumerable<Visa>);
            return View(visas);
        }

        [Authorize("Visa")]
        public IActionResult Visa(int Id)
        {
            var getter = new GetterAll(_getterUtils, _context);
            var visa = (getter.GetAll("Visa") as IEnumerable<Visa>).Where(x => x.VisaID == Id).Single();
            return View(visa);
        }

        [HttpGet]
        [Authorize("Visa")]
        public IActionResult UpdateVisa(int id)
        {
            var getter = new GetterAll(_getterUtils, _context);
            var data = new EditVisaViewModel() { id = id };
            data.paisesNames = getter.GetAll("Pais").Select(x => (x as Pais).Nombre);
            data.regionesName = getter.GetAll("Region").Select(x => (x as Region).Nombre);
            var visa = getter.GetAll("Visa")
                .Select(x => (x as Visa))
                .Where(x => x.VisaID == id)
                .Single();
            data.Nombre = visa.Name;
            data.SelectedPais = visa.Paises != null ? visa.Paises.Select(x => x.Pais.Nombre) : new List<string>();
            data.SelectedRegion = visa.Regiones != null ? visa.Regiones.Select(x => x.Region.Nombre) : new List<string>();
            
            return View(data);
        }

        [HttpPost]
        public IActionResult UpdateVisa(EditVisaViewModel vm)
        {
            AdminService service = new AdminService(_context, _userManager, _getterUtils);
            var getter = new GetterAll(_getterUtils, _context);
            var visas = (IEnumerable<Visa>)getter.GetAll("Visa");
            Visa toUpd = visas.Where(v => v.VisaID == vm.id).Single();
            Visa entity = new Visa()
            {
                Name = vm.Nombre
            };

            if (vm.SelectedRegion != null || vm.SelectedRegion.Count() > 0)
            {
                var regiones = (IEnumerable<Region>)getter.GetAll("Region");
                List<Region_Visa> regiones_visa = new List<Region_Visa>();

                foreach (var name in vm.SelectedRegion)
                    foreach (var region in regiones)
                    {
                        if (region.Nombre == name)
                        {
                            regiones_visa.Add(new Region_Visa() { Region = region, Visa = toUpd });
                            break;
                        }
                    }
                toUpd.Regiones = regiones_visa;
            }

            if (vm.SelectedPais != null || vm.SelectedPais.Count() > 0)
            {
                var paises = (IEnumerable<Pais>)getter.GetAll("Pais");
                List<Pais_Visa> paises_visa = new List<Pais_Visa>();

                foreach (var name in vm.SelectedPais)
                    foreach (var pais in paises)
                    {
                        if (pais.Nombre == name)
                        {
                            paises_visa.Add(new Pais_Visa() { Pais = pais, Visa = toUpd });
                        }
                    }
                toUpd.Paises = paises_visa;
            }

            service.UpdateVisa(entity, toUpd);
            return RedirectToAction("Welcome");
        }
    }
}
