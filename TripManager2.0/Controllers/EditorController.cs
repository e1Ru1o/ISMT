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
using System.Linq;
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
            var data = services.GetItinerariosEstado(
                Enum.Parse<Estado>($"PendienteAprobacion{User.Claims.Where(x => x.Type == "Institucion").Single().Value}"),
                user
            ).Select(x => new UserTripViewModel(x.FechaInicio.Value, x.FechaFin.Value, x.Estado.ToString(), x.ItinerarioID, x.Usuario));
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizeTrip(int tripId, int action)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (action == 0)
                services.ManageActionAprobar(tripId, user.Id, "");
            else if (action == 1)
                services.ManageActionRechazar(tripId, user.Id, "");
            else
                services.CancelItinerario(tripId, user.Id, "");

            return Redirect("AuthorizeTrip");
        }

        [Authorize("Passport")]
        [HttpGet]
        public async Task<IActionResult> AuthorizePassport()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var data = services.GetItinerariosEstado(Estado.PendientePasaporte, user);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizePassport(int itinerarioId, int action)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (action == 0)
                services.SetPassportToUser(itinerarioId, user.Id, "");
            else if (action == 1)
                services.ManageActionRechazarPasaporte(itinerarioId, user.Id, "");
            else
                services.CancelItinerario(itinerarioId, user.Id, "");

            return Redirect("AuthorizePassport");
        }

        [HttpGet]
        [Authorize("Visa")]
        public async Task<IActionResult> GiveVisa()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var data = services.GetItinerariosEstado(Estado.PendienteVisas, user);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> GiveVisa(int itinerarioId, int visaId, int action)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (action == 0)
                services.SetVisaToUser(itinerarioId, visaId, user.Id);
            else if (action == 1)
                services.ManageActionRechazarVisa(itinerarioId, visaId, user.Id);
            else
                services.CancelItinerario(itinerarioId, user.Id, "");
            
            return Redirect("AuthorizeVisa");
        }

        [HttpGet]
        [Authorize("Visa")]
        public IActionResult CreateVisa()
        {
            var getter = new GetterAll(_getterUtils, _context);
            var countries = getter.GetAll("Pais").Select(x => (x as Pais).Nombre);
            var regions = getter.GetAll("Region").Select(x => (x as Region).Nombre);
            return View(new VisaViewModel() { paisesNames = countries, regionesName = regions});
        }

        [HttpPost]
        public IActionResult CreateVisa(VisaViewModel vm)
        {
            //TODO: [TENORIO] save the visa. Remember that one of the two list may be null
            return RedirectToAction("Welcome", "User");
        }


    }
}
