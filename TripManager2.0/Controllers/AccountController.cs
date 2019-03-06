using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using BizLogic.Authentication;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripManager2._0.ViewModels;

namespace TripManager2._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _context;

        public AccountController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Edit(string email)
        {
            var _loginService = new LoginService(_context);
            Usuario user;
            _loginService.TryGetUserByEmail(email, out user);
            //porque al enviarselo al html y editarlo pierdo el id y los permisos
            var cmd = new RegisterUsuarioCommand();
            cmd.SetViewModel(user);
            return View(cmd);
        }

        [HttpPost]
        public IActionResult Edit(RegisterUsuarioCommand cmd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _loginService = new LoginService(_context);
                    _loginService.EditUser(cmd);
                    var user = new UserViewModel()
                    { FirstName = cmd.FirstName, Email = cmd.Email, SecondName = cmd.SecondName, per = cmd.Permission };
                    return RedirectToAction("Welcome", "User", user);
                }
            }
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured trying to update the user"
                    );
            }

            //If we got to here, something went wrong
            return View(cmd);
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
                    var user = new UserViewModel()
                    { FirstName = cmd.FirstName, Email = cmd.Email, SecondName = cmd.SecondName, per = cmd.Permission };
                    return RedirectToAction("Welcome", "User", user);
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
                Usuario user;
                if (ModelState.IsValid && _loginService.TryLoginUsuario(lvm, out user))
                {
                    var userView = new UserViewModel()
                    { FirstName = user.FirstName, Email = user.Email, SecondName = user.SecondName, per = user.Permission };
                    return RedirectToAction("Welcome", "User", userView);
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
