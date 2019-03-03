using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripManager2._0.ViewModels;

namespace TripManager2._0.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Welcome(UserViewModel user)
        {
            return View(user);
        }
    }
}
