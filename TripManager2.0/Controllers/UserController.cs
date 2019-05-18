using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Welcome(TableUserViewModel user)
        {
            user.per = PermisoTipo.admin;
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ViajeViewModel());
        }

        [HttpPost]
        public IActionResult Create(ViajeViewModel travel)
        {

            return null;
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
