using BizDbAccess.GenericInterfaces;
using BizLogic.Authentication;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManager2._0.Controllers
{
    public class AccountController : Controller
    {
        //private readonly RegisterService _registerService;
        //private readonly LoginService _loginService;
        private readonly IUnitOfWork _context;

        //public AccountController(RegisterService registeSservice, LoginService loginService)
        //{
        //    _registerService = registeSservice;
        //    _loginService = loginService;
        //}
        public AccountController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUsuarioCommand());
        }

        [HttpPost]
        public IActionResult Register(RegisterUsuarioCommand cmd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _registerService = new RegisterService(_context);
                    var id = _registerService.RegisterUsuario(cmd);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured trying to register the user"
                    );
            }

            //If we got to here, something went wrong
            return View(cmd);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel lvm)
        {
            var _loginService = new LoginService(_context);
            try
            {
                if (ModelState.IsValid && _loginService.LoginUsuario(lvm))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured trying to login"
                    );
            }

            //If we got to here, something went wrong
            return View(lvm);
        }
    }
}
