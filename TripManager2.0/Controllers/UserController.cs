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
            var notificationsList = new List<string>();
            var InvitadosPropios = new List<string>();
            var InvitadosAjenos = new List<string>();

            if (vm.ViajesUpdated.Any())
            {
                if (User.Claims.Where(c => c.Type == "Visa").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteVisas && v.Usuario.Email != User.Identity.Name
                                                        && !(v.Estado == Estado.Pendiente ||
                                                           v.Estado == Estado.Cancelado ||
                                                           v.Estado == Estado.PendienteRealizacion))
                                                           .ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                        notificationsList.Add($"Viaje del {data[i].FechaInicio} --> {data[i].FechaFin} tiene ahora estado {data[i].Estado}");
                    }
                }

                if (User.Claims.Where(c => c.Type == "Passport").Any())
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendientePasaporte && v.Usuario.Email != User.Identity.Name
                                                        && !(v.Estado == Estado.Pendiente ||
                                                           v.Estado == Estado.Cancelado ||
                                                           v.Estado == Estado.PendienteRealizacion))
                                                           .ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                        notificationsList.Add($"Viaje del {data[i].FechaInicio} --> {data[i].FechaFin} tiene ahora estado {data[i].Estado}");
                    }
                }

                if (User.HasClaim("Institucion", "JefeArea"))
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionJefeArea && v.Usuario.Email != User.Identity.Name
                                                        && !(v.Estado == Estado.Pendiente ||
                                                           v.Estado == Estado.Cancelado ||
                                                           v.Estado == Estado.PendienteRealizacion))
                                                           .ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                        notificationsList.Add($"Viaje del {data[i].FechaInicio} --> {data[i].FechaFin} tiene ahora estado {data[i].Estado}");
                    }
                }

                if (User.HasClaim("Institucion", "Decano"))
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionDecano && v.Usuario.Email != User.Identity.Name
                                                        && !(v.Estado == Estado.Pendiente ||
                                                           v.Estado == Estado.Cancelado ||
                                                           v.Estado == Estado.PendienteRealizacion))
                                                           .ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                        notificationsList.Add($"Viaje del {data[i].FechaInicio} --> {data[i].FechaFin} tiene ahora estado {data[i].Estado}");
                    }
                }

                if (User.HasClaim("Institucion", "Rector"))
                {
                    var data = vm.ViajesUpdated.Where(v => v.Estado == Estado.PendienteAprobacionRector && v.Usuario.Email != User.Identity.Name
                                                       && !(v.Estado == Estado.Pendiente ||
                                                           v.Estado == Estado.Cancelado ||
                                                           v.Estado == Estado.PendienteRealizacion))
                                                           .ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Update = 0;
                        _workflowServices.UpdateItinerario(data[i], data[i]);
                        notificationsList.Add($"Viaje del {data[i].FechaInicio} --> {data[i].FechaFin} tiene ahora estado {data[i].Estado}");
                    }
                }

                var misViajes = vm.ViajesUpdated.Where(v => v.Usuario.Email == User.Identity.Name
                                                         && (v.Estado == Estado.Pendiente ||
                                                             v.Estado == Estado.Cancelado ||
                                                             v.Estado == Estado.PendienteRealizacion))
                                                             .ToList();
                if (misViajes.Count() != 0)
                {
                    for (int i = 0; i < misViajes.Count(); i++)
                    {
                        misViajes[i].Update = 0;
                        _workflowServices.UpdateItinerario(misViajes[i], misViajes[i]);
                    }
                }
            }
            
            if (t.InvitadosUpdated.Any())
            {
                var misInvitados = t.InvitadosUpdated.Where(vi => vi.Usuario.Email == User.Identity.Name
                                                               && (vi.Estado == Estado.Pendiente ||
                                                                   vi.Estado == Estado.Cancelado ||
                                                                   vi.Estado == Estado.PendienteRealizacion))
                                                                   .ToList();
                if (misInvitados.Count != 0)
                {
                    for (int i = 0; i < misInvitados.Count(); i++)
                    {
                        misInvitados[i].Update = 0;
                        _workflowServices.UpdateViajeInvitado(misInvitados[i], misInvitados[i]);
                        InvitadosPropios.Add($"Viaje del invitado {misInvitados[i].Nombre} con fecha {misInvitados[i].FechaLLegada} tiene ahora estado {misInvitados[i].Estado}");
                    }
                }

                var invitados = t.InvitadosUpdated.Where(vi => vi.Usuario.Email != User.Identity.Name).ToList();
                if (invitados.Count != 0)
                {
                    for (int i = 0; i < invitados.Count(); i++)
                    {
                        invitados[i].Update = 0;
                        _workflowServices.UpdateViajeInvitado(invitados[i], invitados[i]);
                        InvitadosAjenos.Add($"Viaje del invitado {invitados[i].Nombre} con fecha {invitados[i].FechaLLegada} tiene ahora estado {invitados[i].Estado}");
                    }
                }
            }

            vm.NotificationsList = notificationsList;
            vm.InvitadosPropios = InvitadosPropios;
            vm.InvitadosAjenos = InvitadosAjenos;
            
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

            return RedirectToAction("ViewTrips");
        }

        [HttpGet]
        public async Task<IActionResult> ViewTrips()
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            var vm = new PendingTripViewModel();
            vm.Users = services.GetItinerarioNotFinished(user)
                .Select(x => new TripViewModel(x.FechaInicio.Value, x.FechaFin.Value, x.Estado.ToString(), x.ItinerarioID));
            vm.Visitants = services.GetViajesInvitadosNotFinished(user)
                .Select(x => new InvitationViewModel(x));
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ViewTrips(int vId, int action, int uType)
        {
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            if (uType == 0)
            {
                if (action == 0)
                    services.CancelItinerario(vId, user.Id, "El usuario cancelo su viaje");
                else if (action == 1)
                    services.ContinuarItinerario(vId);
                else
                    services.RealizarItinerario(vId);
            }
            else
            {
                if (action == 0)
                    services.CancelViajeInvitado(vId, user.Id, "El usuario cancelo su viaje");
                else if (action == 1)
                    services.ContinuarViajeInvitado(vId);
                else
                    services.RealizarViajeInvitado(vId);
            }

            return RedirectToAction("ViewTrips");
        }

        [HttpGet]
        [Authorize("Institucion")]
        public IActionResult Invitation()
        {
            return View(new InvitationViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Invitation(InvitationViewModel vm)
        {
            WorkflowServices services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);

            int id = services.RegisterViajeInvitado(user.Id, vm.Name, vm.Procedencia, vm.Motivo, vm.End);

            if (id != -1)
                services.CreateViajeInvitadoWorkflow(id, User.Claims.Where(x => x.Type == "Institucion").Single().Value);

            return RedirectToAction("Welcome");
        }
        public IActionResult Viaje(int id)
        {

            GetterAll getter = new GetterAll(_getterUtils, _context);
            var it = (getter.GetAll("Itinerario") as IEnumerable<Itinerario>).Where(x => x.ItinerarioID == id).Single();
            var viajes = (getter.GetAll("Viaje") as IEnumerable<Viaje>).Where(x => x.Itinerario.ItinerarioID == id);
            return View(viajes);
        }

        public IActionResult Historial()
        {

            WorkflowServices services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            return View(services.GetHistorial());
        }
        public IActionResult ViajeInvitado(int id)
        {

            WorkflowServices services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            return View(services.GetViajeInvitado(id));
        }
    }
}


