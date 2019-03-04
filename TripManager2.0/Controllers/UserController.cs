using Microsoft.AspNetCore.Mvc;
using TripManager2._0.ViewModels;

namespace TripManager2._0.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Welcome(TableUserViewModel user)
        {
            return View(user);
        }

        public IActionResult Print(TableUserViewModel text)
        {
            return Content(text.Table.ToString());
        }
    }
}
