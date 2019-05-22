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
        public async Task<IActionResult> AuthorizeTrip()
        {
            //TODO: [TENORIO] add an access restriction to this method that verify [Authorize("Institucion") where Value != "Trabajador"]
            var services = new WorkflowServices(_context, _userManager, _getterUtils, _signInManager);
            var user = await _userManager.GetUserAsync(User);
            //TODO: [Juanda] put on data the Itinerarios collection correspondent to Itinerarios that user may approbe
            
            //(user, $"aprobacion de {User.Claims.Where(x => x.Type == "Institucion").Single().Value}");
            var data = new List<Itinerario>();
            return View(data);
        }

        [HttpPost]
        public IActionResult AuthorizeTrip(int id)
        {
            return Redirect("AuthorizeTrip");
        }
    }
}
