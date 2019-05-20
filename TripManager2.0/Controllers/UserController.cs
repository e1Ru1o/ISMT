using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TripManager2._0.ViewModels;

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
        public IActionResult Create(ViajeViewModel travel)
        {
            //Teno proccess the data
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
    }
}
