using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizDbAccess.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using TripManager2._0.ViewModels;

namespace TripManager2._0.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UserController(IUnitOfWork context,
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _context = context;
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
            //falta completar el diccionario
            var getter = new GetterAll(new Dictionary<string, string> { { "Usuario", "UserDbAccess" } },
                new AssemblyName("BizData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"),
                _context, _signInManager, _userManager);
            var result1 = getter.GetAll(text.Table.ToString());

            return View($"Display{text.Table.ToString()}", result1);
        }
    }
}
